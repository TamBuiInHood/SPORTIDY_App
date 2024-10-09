import React, { useState, useEffect } from 'react';
import { View, Text, Image, StyleSheet, ScrollView, ActivityIndicator, TouchableOpacity, Dimensions } from 'react-native';
import { NativeBaseProvider, Button } from 'native-base';
import api from '../../config/axios';
import { RouteProp, NativeStackNavigationProp } from '@react-navigation/native-stack';
import { RootStackParamList } from '@/types/types';

const { width } = Dimensions.get('window');

type ClubScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'ClubDetail'>;
type ClubScreenRouteProp = RouteProp<RootStackParamList, 'ClubDetail'>;

const ClubDetailScreen = ({ navigation, route }: { navigation: ClubScreenNavigationProp; route: ClubScreenRouteProp }) => {
    const { club } = route.params;
    const [clubDetails, setClubDetails] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [isJoined, setIsJoined] = useState(false);

    useEffect(() => {
        const fetchClubDetails = async () => {
            try {
                // Không cần fetch nữa, thay bằng data mẫu
                const mockClubDetails = {
                  clubId: 1,
                  clubCode: "PSG-FC",
                  clubName: "Paris Saint-Germain FC",
                  regulation: "Chưa có thông tin",
                  information: "Thông tin về câu lạc bộ Paris Saint-Germain FC.",
                  slogan: "Chơi cùng đam mê, chiến thắng rực rỡ!",
                  mainSport: "Football",
                  createDate: "2024-01-01T00:00:00Z",
                  location: "Thủ Đức, Quận 9",
                  totalMember: 100,
                  avatarClub: "https://example.com/images/avatar-paris-sg.jpg",
                  coverImageClub: "https://example.com/images/cover-paris-sg.jpg",
                  listMember: []
                };
                setClubDetails(mockClubDetails);

                // Kiểm tra trạng thái tham gia (gọi API riêng nếu cần)
                // setIsJoined(isMemberResponse.data.isMember);
                setIsJoined(false); // Giả sử chưa tham gia
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
            // const response = await api.post(`/Club/join/${club.clubId}`);
            // if (response.status === 200) {
            //   setIsJoined(true); // Cập nhật trạng thái isJoined
            // }
            setIsJoined(true); // Thay bằng cập nhật trực tiếp
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

    // Hiển thị chi tiết club
    if (clubDetails) {
        return (
            <ScrollView style={styles.container}>
                <Image source={{ uri: clubDetails.coverImageClub }} style={styles.coverImage} />
                <Text style={styles.clubName}>{clubDetails.clubName}</Text>
                {/* Hiển thị các thông tin khác của club */}

                <View style={styles.clubInfo}>
                    <Text style={styles.clubInfoText}>
                        <Text style={styles.label}>Slogan:</Text> {clubDetails.slogan}
                    </Text>
                    <Text style={styles.clubInfoText}>
                        <Text style={styles.label}>Mô tả:</Text> {clubDetails.information}
                    </Text>
                    <Text style={styles.clubInfoText}>
                        <Text style={styles.label}>Thể thao chính:</Text> {clubDetails.mainSport}
                    </Text>
                    <Text style={styles.clubInfoText}>
                        <Text style={styles.label}>Địa điểm:</Text> {clubDetails.location}
                    </Text>
                    <Text style={styles.clubInfoText}>
                        <Text style={styles.label}>Số lượng thành viên:</Text> {clubDetails.totalMember}
                    </Text>
                    <Text style={styles.clubInfoText}>
                        <Text style={styles.label}>Ngày thành lập:</Text> {clubDetails.createDate}
                    </Text>
                </View>

                {/* Danh sách các trận đấu sắp diễn ra (cần fetch từ API) */}
                {/* ... match list */}

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

            </ScrollView>
        );
    }
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
    },
    clubInfo: {
        paddingHorizontal: 16,
    },
    clubInfoText: {
        fontSize: 16,
        marginBottom: 8,
    },
    label: {
        fontWeight: 'bold',
    },
});

export default ClubDetailScreen;