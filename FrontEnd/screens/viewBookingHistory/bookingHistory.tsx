import React, { useEffect, useState } from 'react';
import { View, Text, FlatList, StyleSheet, TouchableOpacity, ActivityIndicator } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import AsyncStorage from '@react-native-async-storage/async-storage';
import axios from 'axios';
import { useNavigation, useRoute } from '@react-navigation/native';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RootStackParamList } from '@/types/types';

type PaymentScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "MyHistory">;

const MyHistory: React.FC = () => {
    const [bookings, setBookings] = useState<any[]>([]);
    const [loading, setLoading] = useState(false);
    const navigation = useNavigation<PaymentScreenNavigationProp>();
    const route = useRoute();

    // Function to format time
    const formatTime = (date: string) => {
        const parsedDate = new Date(date);
        return parsedDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true }); // 12-hour format with AM/PM
    };

    const formatDate = (date: string) => {
        const parsedDate = new Date(date);
        const day = parsedDate.getDate().toString().padStart(2, '0');
        const month = (parsedDate.getMonth() + 1).toString().padStart(2, '0'); // Months are 0-based
        const year = parsedDate.getFullYear();
        return `${day}/${month}/${year}`; 
    };
    // Fetch bookings from API
    const fetchBookings = async () => {
        setLoading(true);
        try {
            const userId = await AsyncStorage.getItem('userId');
            if (userId) {
                const response = await axios.get(`https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/bookings/get-by-user-id?currIdLogin=${userId}`);
    
                console.log('Response Data:', response.data); // Log the full response
    
                // Correctly extract the bookings
                const fetchedBookings = response.data.data.list || [];
    
                // Sort bookings by the most recent dateStart
                const sortedBookings = fetchedBookings.sort((a: any, b: any) => new Date(b.dateStart).getTime() - new Date(a.dateStart).getTime());
    
                console.log('Sorted Bookings:', sortedBookings); // Log the sorted bookings
                setBookings(sortedBookings);
            }
        } catch (error: any) {
            console.error('Error fetching bookings:', error.response?.data || error.message);
        } finally {
            setLoading(false);
        }
    };
    
    useEffect(() => {
        fetchBookings();
    }, []);

    const renderBookingItem = ({ item }: { item: any }) => {
        let statusColor;
        let statusText;

        // Define the status display and color based on item.status
        if (item.status === 3) {
            statusText = 'Used';
            statusColor = 'green';
        } else if (item.status === 4) {
            statusText = 'Cancel';
            statusColor = 'red';
        } else if (item.status === 2) {
            statusText = 'Not yet';
            statusColor = '#ffa500'; 
        } else {
            statusText = 'Other';
            statusColor = '#5991FF'; // Default color
        }


        return (
            <TouchableOpacity
                style={styles.bookingItem}
                onPress={() => navigation.navigate('HistoryDetail', { booking: item })}
            >
                <View style={styles.header}>
                    <Ionicons name="football-outline" size={24} color="#ff951d" style={styles.icon} />
                    <Text style={[styles.status, { backgroundColor: statusColor }]}>{statusText}</Text>
                </View>
                <Text style={styles.playfieldName}>{item.playField.playfieldName}</Text>
                <Text>Invoice Number: {item.bookingCode}</Text>
                <Text>Address: {item.playField.address}</Text>
                <Text>Date: {formatDate(item.dateStart)}</Text>
                <Text>Time: {formatTime(item.dateStart)} - {formatTime(item.dateEnd)}</Text>
                <View style={styles.priceContainer}>
                    <Text style={styles.price}>Price: {item.price.toLocaleString()} VND</Text>
                </View>
            </TouchableOpacity>
        );
    };

    if (loading) {
        return (
            <View style={styles.loader}>
                <ActivityIndicator size="large" color="#ff951d" />
            </View>
        );
    }

    return (
        <View style={styles.container}>
        <TouchableOpacity 
            style={styles.backButton} 
            onPress={() => navigation.goBack()}
        >
            <Ionicons 
                name='arrow-back-circle' 
                color={"orange"} 
                size={30}  // Increase size for better visibility
            />
        </TouchableOpacity>
        <Text style={styles.title}>My Booking History</Text>
        <FlatList
            data={bookings}
            renderItem={renderBookingItem}
            keyExtractor={(item) => item.invoiceNumber}
            ListEmptyComponent={() => <Text>No bookings available</Text>}
        />
    </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#fff',
        justifyContent: 'flex-start', 
    },
    title: {
        fontSize: 24,
        fontWeight: 'bold',
        textAlign: 'center',
        marginVertical: 20,
        backgroundColor: '#ff951d',
        paddingVertical: 10,
        color: '#ffff',
    },
    backButton: {
        flexDirection: 'row',
        alignItems: 'center',
        paddingLeft: 10,  // Space from the left edge of the screen
        paddingVertical: 10,  // Vertical padding around the icon
    },
    bookingItem: {
        borderWidth: 1,
        borderColor: '#ddd',
        borderRadius: 10,
        padding: 15,
        marginBottom: 15,
        position: 'relative',
    },
    playfieldName: {
        fontSize: 18,
        fontWeight: 'bold',
        color: '#ff951d',
    },
    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'flex-start',
    },
    icon: {
        marginRight: 10,
    },
    status: {
        color: 'white',
        padding: 5,
        borderRadius: 10,
        textAlign: 'center',
        marginTop: 0,
        position: 'absolute',
        right: 15,
        top: 15,
        fontSize: 12,
    },
    priceContainer: {
        borderTopWidth: 1,
        borderTopColor: '#ddd',
        marginTop: 10,
        paddingTop: 10,
    },
    price: {
        fontSize: 16,
        fontWeight: 'bold',
        color: '#ff951d',
    },
    loader: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
    },
});

export default MyHistory;
