import React, { useState, useEffect } from 'react';
import { View, Text, Image, StyleSheet, ScrollView, ActivityIndicator, TouchableOpacity, Dimensions } from 'react-native';
import { NativeBaseProvider, Button } from 'native-base';
import api from '../../config/axios';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { RootStackParamList } from '@/types/types';
import { RouteProp } from '@react-navigation/native';

const { width } = Dimensions.get('window');

type ClubScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'ClubDetail'>;
type ClubScreenRouteProp = RouteProp<RootStackParamList, 'ClubDetail'>;

const ClubDetailScreen = ({ navigation, route }: { navigation: ClubScreenNavigationProp; route: ClubScreenRouteProp }) => {
    const { club } = route.params;
    const [clubDetails, setClubDetails] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [isJoined, setIsJoined] = useState(false);
    const [activeTab, setActiveTab] = useState('Post');

    useEffect(() => {
        const fetchClubDetails = async () => {
            try {
                // Dữ liệu mẫu
                const mockClubDetails = {
                    clubId: 1,
                    clubCode: "PSG-FC",
                    clubName: "Paris Saint-Germain FC",
                    regulation: "Quy định câu lạc bộ Paris Saint-Germain FC.",
                    information: "Thông tin về câu lạc bộ Paris Saint-Germain FC.",
                    slogan: "Chơi cùng đam mê, chiến thắng rực rỡ!",
                    mainSport: "Football",
                    createDate: "2024-01-01T00:00:00Z",
                    location: "Thủ Đức, Quận 9",
                    totalMember: 100,
                    avatarClub: "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.pngegg.com%2Fvi%2Fpng-vhrfs&psig=AOvVaw1cZe2DKfNtJw-0G9GQn_vl&ust=1728535326958000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCIDH-YK-gIkDFQAAAAAdAAAAABAE",
                    coverImageClub: "https://cdn.builder.io/api/v1/image/assets/TEMP/cac9029126bb52849507b53ea4ecb5f6ca3e31522c984956a2339b1c1931935b?placeholderIfAbsent=true&apiKey=f8ccf21cf8ce4053b555f169e17dcf6a",
                    listMember: []
                };
                setClubDetails(mockClubDetails);
                setIsJoined(false);
            } catch (err) {
                setError(err);
                console.error("Error fetching club details:", err);
            } finally {
                setLoading(false);
            }
        };

        fetchClubDetails();
    }, [club.clubId]);

    const handleJoinClub = async () => {
        try {
            // Gọi API để tham gia club (nếu cần)
            setIsJoined(true);
        } catch (error) {
            setError(error);
            console.error('Lỗi khi tham gia câu lạc bộ', error);
        }
    };

    const handleTabPress = (tabName) => {
        setActiveTab(tabName);
    };

    if (loading) {
        return <ActivityIndicator size="large" style={styles.loadingIndicator} />;
    }

    if (error) {
        return (
            <View style={styles.errorContainer}>
                <Text style={styles.errorText}>Error: {error.message}</Text>
            </View>
        );
    }

    return (
        <NativeBaseProvider>
            <ScrollView style={styles.container}>
                <Image source={{ uri: clubDetails.coverImageClub }} style={styles.coverImage} />
                <View style={styles.header}>
                    <Text style={styles.clubName}>{clubDetails.clubName}</Text>
                    <View style={styles.headerRight}>
                        <Text style={styles.memberCount}>{clubDetails.totalMember} thành viên</Text>
                        {isJoined ? (
                            <Button variant="outline" style={styles.memberButton} disabled>Member</Button>
                        ) : (
                            <Button variant="outline" style={styles.joinButton} onPress={handleJoinClub}>Join in</Button>
                        )}
                    </View>
                </View>

                <View style={styles.tabsContainer}>
                    <TouchableOpacity style={[styles.tab, activeTab === 'Post' && styles.activeTab]} onPress={() => handleTabPress('Post')}>
                        <Text style={[styles.tabText, activeTab === 'Post' && styles.activeTabText]}>Post</Text>
                    </TouchableOpacity>
                    <TouchableOpacity style={[styles.tab, activeTab === 'Information' && styles.activeTab]} onPress={() => handleTabPress('Information')}>
                        <Text style={[styles.tabText, activeTab === 'Information' && styles.activeTabText]}>Information</Text>
                    </TouchableOpacity>
                </View>

                {activeTab === 'Post' && (
                    <View>
                        {/* ... Nội dung cho tab "Post" */}
                    </View>
                )}
                {activeTab === 'Information' && (
                    <View style={styles.infoContainer}>
                        <Text style={styles.infoText}>Members: {clubDetails.totalMember}</Text>
                        <Text style={styles.infoText}>Main Sport: {clubDetails.mainSport}</Text>
                        <Text style={styles.infoText}>Location: {clubDetails.location}</Text>
                        <Text style={styles.regulationTitle}>Regulation</Text>
                        <Text style={styles.infoText}>{clubDetails.regulation}</Text>
                    </View>
                )}

            </ScrollView>
        </NativeBaseProvider>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',
    },
    coverImage: {
        width: width,
        height: 200,
    },
    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        paddingHorizontal: 16,
        paddingVertical: 16,
    },
    headerRight: {
        flexDirection: 'row',
        alignItems: 'center',
    },
    clubName: {
        fontSize: 24,
        fontWeight: 'bold',
        flex:1
    },
    memberCount: {
        fontSize: 16,
        color: '#888',
    },
    joinButton: {
        backgroundColor: '#FFCC00',
        marginLeft: 16,
    },
    memberButton: {
        backgroundColor: '#ddd',
        marginLeft: 16,

    },
    tabsContainer: {
        flexDirection: 'row',
        justifyContent: 'space-around',
        borderBottomWidth: 1,
        borderBottomColor: '#ccc',
        paddingVertical: 8,
        marginTop: 16,
    },
    tab: {
        paddingHorizontal: 16,
        paddingVertical: 8,
    },
    activeTab: {
        borderBottomWidth: 2,
        borderBottomColor: '#FFCC00',
    },
    tabText: {
        fontSize: 16,
        color: '#333',
    },
    activeTabText: {
        color: '#FFCC00',
        fontWeight: 'bold',
    },
    infoContainer: {
        marginTop: 8,
        paddingHorizontal: 16,
    },
    regulationTitle: {
        fontSize: 16,
        fontWeight: 'bold',
        marginTop: 16,
    },
    infoText: {
        fontSize: 14,
        lineHeight: 20,
        marginTop: 4,
    },
    loadingIndicator: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
    },
    errorContainer: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
    },
    errorText: {
        color: 'red',
    },
});

export default ClubDetailScreen;