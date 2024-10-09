import React from 'react';
import { View, Text, FlatList, StyleSheet, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RootStackParamList } from '@/types/types';
import { useNavigation, useRoute } from '@react-navigation/native';

type PaymentScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "MyHistory">;

const MyHistory: React.FC = ({ route: any }) => {
    const navigation = useNavigation<PaymentScreenNavigationProp>();
    const route = useRoute();
    const formatTime = (date: string) => {
        const parsedDate = new Date(date);
        return parsedDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
      };
    // Access bookings from route params and safely handle undefined
    const passedBookings = route.params?.bookings || [];
    const fakeBookings = [
        {
            playfieldName: 'Sân cầu lông Hòa Bình',
            invoiceNumber: '4327878',
            address: 'Dĩ An, Bình Dương',
            dateStart: "2024-09-20T15:30:00",
            dateEnd: "2024-09-20T16:30:00",
            price: 100000,
            status: 'Used',
        },
        {
            playfieldName: 'Bóng chuyền Xuân Thủy',
            invoiceNumber: '843948',
            address: '123 Linh Trung, Thủ Đức',
            dateStart: "2024-10-09T10:30:00",
            dateEnd: "2024-10-09T11:30:00",
            price: 200000,
            status: 'Cancel',
        },
    ];
    const allBookings = [...passedBookings, ...fakeBookings];

    const renderBookingItem = ({ item }: { item: any }) => { // Sửa ở đây
        let statusColor;
        if (item.status === 'Used') {
            statusColor = 'green';
        } else if (item.status === 'Cancel') {
            statusColor = 'red';
        } else {
            statusColor = '#5991FF';
        }

        return (
            <TouchableOpacity
                style={styles.bookingItem}
                onPress={() => navigation.navigate('HistoryDetail', { booking: item })} // Chuyển đến màn hình chi tiết
            >
                <View style={styles.header}>
                    <Ionicons name="football-outline" size={24} color="#ff951d" style={styles.icon} />
                    <Text style={[styles.status, { backgroundColor: statusColor }]}>{item.status}</Text>
                </View>
                <Text style={styles.playfieldName}>{item.playfieldName}</Text>
                <Text>Invoice Number: {item.invoiceNumber}</Text>
                <Text>Address: {item.address}</Text>
                <Text>Time: {formatTime(item.dateStart)} - {formatTime(item.dateEnd)}</Text>
                <View style={styles.priceContainer}>
                    <Text style={styles.price}>Price: {item.price.toLocaleString()} VND</Text>
                </View>
            </TouchableOpacity>
        );
    };

    return (
        <View style={styles.container}>
            <Text style={styles.title}>My Booking History</Text>
            <FlatList
                data={allBookings}
                renderItem={renderBookingItem}
                keyExtractor={(item) => item.invoiceNumber}
            />
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#fff',
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
    bookingItem: {
        borderWidth: 1,
        borderColor: '#ddd',
        borderRadius: 10,
        padding: 15,
        marginBottom: 15,
        position: 'relative', // Required for absolute positioning of status
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
        marginTop: 0, // Remove margin top to align better
        position: 'absolute',
        right: 15,
        top: 15,
        fontSize: 12, // Make status text smaller
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
});

export default MyHistory;
