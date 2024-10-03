import React, { useEffect, useState } from 'react';
import { StyleSheet, View, Text, TouchableOpacity, Image, ScrollView } from 'react-native';
import DetailHeader from '../../components/DetailHeader';
import { Ionicons } from '@expo/vector-icons';
import Discussion from '../discussion/discussion';
import { Card, Club, EventDetailRouteProp, MeetingDetail } from '@/types/types';
import { useRoute } from '@react-navigation/native';
import api from '@/config/api';

const EventDetailScreen = () => {
    const route = useRoute<EventDetailRouteProp>();
    const meetingId = route.params.meetingId;

    const [joined, setJoined] = useState(false);
    const [activeTab, setActiveTab] = useState<'details' | 'discussion'>('details');
    const [meeting, setMeeting] = useState<Card | null>(null);
    const [club, setClub] = useState<Club | null>(null);
    const [userId, setUserId] = useState(3);

    useEffect(() => {
        const fetchMeeting = async () => {
            try {
                const response = await api.getMeetingById(meetingId);
                const result: Card = response.data;
                setMeeting(result);
            } catch (error) {
                console.error('Failed to fetch meeting:', error);
                setMeeting(null);
            }
        };

        fetchMeeting();
    }, [meetingId]);

    if (!meeting) {
        return <Text>Loading...</Text>;
    }
    const handleJoinMeeting = async () => {
        if (!meeting || !meeting.clubId || !userId) {
            console.log('Missing data:', { meeting, club:meeting?.clubId, userId });
            return;
        }
        try {
            const response = await api.joinMeeting(userId, meeting.clubId, meeting.meetingId);
            console.log('Successfully joined the meeting:', response.data);
            setJoined(true); 
        } catch (error) {
            console.error('Error joining the meeting:', error.response?.data || error.message);
        }
    };
    return (
        <ScrollView style={styles.container}>
            <DetailHeader
                date={new Date(meeting.startDate).toLocaleDateString('en-US', {
                    month: 'long', day: 'numeric', year: 'numeric'
                })}
                title={meeting.meetingName || 'Event Title'}
                activeTab={activeTab}
                onTabChange={setActiveTab}
            />
            {activeTab === 'details' && (
                <View>
                    <View style={styles.clubInfoSection}>
                        <Image source={{ uri: meeting.meetingImage || 'default_image_url_here' }} style={styles.clubImage} />
                        <View>
                            <Text style={styles.clubName}>{meeting.clubName || 'Undefined'}</Text>
                            <Text style={styles.clubFrequency}>{meeting.totalMember} members</Text>
                        </View>
                        {joined && (
                            <View style={styles.joinedBadge}>
                                <Text style={styles.joinedBadgeText}>Joined</Text>
                            </View>
                        )}
                    </View>

                    <View style={styles.hostSection}>
                        <Text style={styles.hostTitle}>Host</Text>
                        <View style={styles.hostAvatarContainer}>
                            <Image source={{ uri: meeting.meetingImage }} style={styles.hostAvatar} />
                            <View style={styles.placeholderAvatar} />
                            <View style={styles.placeholderAvatar} />
                            <View style={styles.placeholderAvatar} />
                            <View style={styles.placeholderAvatar} />
                        </View>
                    </View>

                    <View style={styles.detailsSection}>
                        <View style={styles.row}>
                            <Ionicons name='calendar-clear-outline' size={25} style={styles.icon} />
                            <Text style={styles.eventDate}>{new Date(meeting.startDate).toLocaleString()}</Text>
                        </View>

                        <TouchableOpacity>
                            <Text style={styles.eventDuration}>
                                {(new Date(meeting.endDate).getTime() - new Date(meeting.startDate).getTime()) / 3600000} hour(s)
                            </Text>
                        </TouchableOpacity>

                        <View style={styles.row}>
                            <Ionicons name='location-outline' size={25} style={styles.icon} />
                            <Text style={styles.locationText}>{meeting.address}</Text>
                        </View>

                        <View style={styles.row}>
                            <Ionicons name='close-circle-outline' size={25} style={styles.icon} />
                            <Text style={styles.cancellationInfo}>
                                Cancellation freeze {meeting.cancelBefore} hour(s) before start
                            </Text>
                        </View>
                    </View>

                    <View style={styles.notesSection}>
                        <Text style={styles.notesTitle}>Notes</Text>
                        <Text style={styles.note}>{meeting.note}</Text>
                    </View>

                    {!joined ? (
                        <TouchableOpacity style={styles.joinButton} onPress={handleJoinMeeting}>
                            <Text style={styles.joinButtonText}>Join Meet</Text>
                        </TouchableOpacity>
                    ) : (
                        <View style={styles.joinedContainer}>
                            <TouchableOpacity onPress={() => setJoined(false)}>
                                <Text style={styles.cancelButtonText}>Cancel</Text>
                            </TouchableOpacity>
                        </View>
                    )}
                </View>
            )}
            {activeTab === 'discussion' && <Discussion meetingId={meetingId} />}
        </ScrollView>
    );
};
const styles = StyleSheet.create({
    detailHeader: {
        position: 'absolute',
        top: 0,
        width: '100%',
        zIndex: 1,
    },
    container: {
        flex: 1,
        backgroundColor: '#f7f7f7',
        paddingHorizontal: 0,
        marginTop: 0,
    },
    header: {
        backgroundColor: '#FFB41C',
        padding: 20,
        marginBottom: 15,
        alignItems: 'center',
        justifyContent: 'center',
    },
    headerDate: {
        fontSize: 14,
        color: '#FF915D',
        marginTop: 30,
        fontWeight: "bold",
        textAlign: 'center',
    },
    headerTitle: {
        fontSize: 19,
        color: '#fff',
        fontWeight: 'bold',
        marginBottom: 15,
        textAlign: 'center',
    },
    tabContainer: {
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'center',
        marginLeft: 50
    },
    activeTab: {
        paddingHorizontal: 15,
        backgroundColor: '#FFD01C',
        paddingVertical: 10,
        borderRadius: 10,
        alignItems: 'center',
    },
    inactiveTab: {
        flex: 1,
        backgroundColor: 'transparent',
        paddingVertical: 10,
        alignItems: 'center',
    },
    tabTextActive: {
        color: '#fff',
        fontWeight: 'bold',
        fontSize: 15
    },
    tabTextInactive: {
        color: '#fff',
        fontWeight: 'bold',
        fontSize: 15

    },
    icon: {
        marginRight: 10,
    },
    clubInfoSection: {
        flexDirection: 'row',
        alignItems: 'center',
        padding: 20,
        backgroundColor: "#FFDE59",
        marginTop: -15,
        position: 'relative'
    },
    clubImage: {
        width: 50,
        height: 50,
        borderRadius: 25,
        marginRight: 15,
    },
    clubName: {
        fontSize: 16,
        fontWeight: 'bold',
    },
    clubFrequency: {
        fontSize: 14,
        color: '#6c757d',
    },
    joinedBadge: {
        position: 'absolute',
        right: 15,
        top: '50%',
        backgroundColor: '#4CAF50',
        borderRadius: 15,
        paddingVertical: 5,
        paddingHorizontal: 10,
    },
    joinedBadgeText: {
        color: '#fff',
        fontSize: 14,
        fontWeight: 'bold',
    },
    joinButton: {
        backgroundColor: '#ffb233',
        borderRadius: 30,
        alignItems: 'center',
        marginHorizontal: 90,
        paddingVertical: 10,
    },
    joinButtonText: {
        color: '#fff',
        fontSize: 16,
        fontWeight: 'bold',
    },
    joinedContainer: {
        backgroundColor: '#FF5252',
        borderRadius: 30,
        alignItems: 'center',
        marginHorizontal: 90,
        paddingVertical: 10,
    },
    cancelButtonText: {
        color: '#fff',
        fontSize: 16,
        fontWeight: 'bold',
    },
    hostSection: {
        margin: 20,

    },
    hostTitle: {
        fontSize: 16,
        fontWeight: 'bold',
        marginBottom: 10,
    },
    hostAvatarContainer: {
        flexDirection: 'row',
    },
    hostAvatar: {
        width: 40,
        height: 40,
        borderRadius: 20,
        marginRight: 10,
    },
    placeholderAvatar: {
        width: 40,
        height: 40,
        borderRadius: 20,
        backgroundColor: '#ddd',
        marginRight: 10,
    },
    detailsSection: {
        marginVertical: 30,
        marginHorizontal: 25,
    },
    eventDate: {
        fontSize: 16,
        fontWeight: 'bold',

    },
    eventDuration: {
        fontSize: 14,
        color: '#6c757d',
        marginBottom: 2,
        marginLeft: 35
    },
    addToCalendar: {
        color: '#007bff',
        fontSize: 14,
        marginBottom: 15,
        fontWeight: 'bold',
    },
    locationText: {
        fontSize: 16,
        fontWeight: 'bold',
        marginTop: 30
    },
    locationDetail: {
        fontSize: 14,
        color: '#6c757d',
    },
    cancellationInfo: {
        fontSize: 16,
        color: '#141415',
        marginTop: 40,
        fontWeight: 'bold',
    },
    notesSection: {
        paddingHorizontal: 15, // Tăng khoảng cách hai bên
        marginTop: -5,
    },
    notesTitle: {
        fontSize: 16,
        fontWeight: 'bold',
        marginBottom: 10,
    },
    note: {
        fontSize: 14,
        color: '#6c757d',
        marginBottom: 5,
    },
    row: {
        flexDirection: 'row',
        alignItems: 'center',
        marginBottom: 10, // Thêm margin để cả icon và text di chuyển cùng nhau
    },
});

export default EventDetailScreen;