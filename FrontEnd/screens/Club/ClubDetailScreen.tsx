import React, { useState, useEffect } from 'react';
import { View, Text, Image, StyleSheet, ScrollView, ActivityIndicator, TouchableOpacity, Dimensions } from 'react-native';
import api from '../../config/axios';


const { width } = Dimensions.get('window');


const ClubDetailScreen = ({ route, navigation }) => {
    const { club } = route.params;
    const [clubDetails, setClubDetails] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [isJoined, setIsJoined] = useState(false);


    useEffect(() => {
        const fetchClubDetails = async () => {
            try {
                const response = await api.get(`/clubs/${club.clubId}`);
                setClubDetails(response.data);


                const isMemberResponse = await api.get(`/me/clubs/${club.clubId}`);
                setIsJoined(isMemberResponse.data.isMember);
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
            const response = await api.post(`/Club/join/${club.clubId}`);
            if (response.status === 200) {
                setIsJoined(true);
            }
        } catch (error) {
            setError(error);
            console.error('Lỗi khi tham gia câu lạc bộ', error);
        }
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
        <ScrollView style={styles.container}>
            <Image source={{ uri: clubDetails?.coverImageClub }} style={styles.coverImage} />
            <View style={styles.clubInfoContainer}>
                <Image source={{ uri: clubDetails?.avatarClub }} style={styles.avatarImage} />
                <View style={styles.clubNameContainer}>
                    <Text style={styles.clubName}>{clubDetails?.clubName}</Text>
                    <Text style={styles.membercount}>{clubDetails?.totalMember} players</Text>
                </View>
            </View>


            <View style={styles.sportInfoContainer}>
                <Image source={require('../../assets/images/sports.png')} style={styles.icon} />
                <Text style={styles.sportInfoText}>{clubDetails?.mainSport}</Text>
            </View>


            <View style={styles.locationInfoContainer}>
                <Image source={require('../../assets/images/location.png')} style={styles.icon} />
                <Text style={styles.locationInfoText}>{clubDetails?.location}</Text>
            </View>




            <View style={styles.buttonContainer}>
                {isJoined ? (
                    <TouchableOpacity style={styles.joinedButton} disabled>
                        <Text style={styles.joinedButtonText}>Member</Text>
                    </TouchableOpacity>
                ) : (
                    <TouchableOpacity style={styles.joinButton} onPress={handleJoinClub}>
                        <Text style={styles.joinButtonText}>Join in</Text>
                    </TouchableOpacity>
                )}
            </View>


            {isJoined && (
                <View>
                    {/* ... Nội dung cho màn hình "After Join" */}


                    <TouchableOpacity onPress={() => navigation.navigate('Introduction')} style={styles.introduc}>
                        <Text>Introduction</Text>
                    </TouchableOpacity>


                </View>
            )}




        </ScrollView>
    );
};



const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',


    },
    coverImage: {
        width,
        height: 200,


    },
    clubInfoContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        padding: 16,


    },
    avatarImage: {
        width: 80,
        height: 80,
        borderRadius: 40,
        marginRight: 16,


    },
    clubNameContainer: {
        justifyContent: 'center'


    },


    clubName: {
        fontSize: 20,
        fontWeight: 'bold',


    },
    sportInfoContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        paddingHorizontal: 16,
        marginTop: 8,


    },
    locationInfoContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        paddingHorizontal: 16,
        marginTop: 4,


    },


    icon: {
        width: 20,
        height: 20,
        marginRight: 8,


    },
    sportInfoText: {
        fontSize: 16,


    },
    locationInfoText: {
        fontSize: 16,


    },
    buttonContainer: {
        padding: 16,


    },
    joinButton: {
        backgroundColor: '#FFCC00',
        padding: 12,
        borderRadius: 8,
        alignItems: 'center',
        marginTop: 16,


    },
    joinButtonText: {
        color: 'white',
        fontWeight: 'bold',


    },
    joinedButton: {
        backgroundColor: '#ddd',
        padding: 12,
        borderRadius: 8,
        alignItems: 'center',
        marginTop: 16,


    },
    joinedButtonText: {
        color: '#888',
        fontWeight: 'bold',


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
    introduc: {
        borderColor: 'black',
        borderWidth: 1,
        marginTop: 15,
        borderRadius: 10,
        alignItems: 'center',
        padding: 10
    },
    membercount: {
        color: 'gray'
    }
});



export default ClubDetailScreen;