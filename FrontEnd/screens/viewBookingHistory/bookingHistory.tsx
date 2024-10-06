import React from 'react';
import { View, Text, FlatList, StyleSheet, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RootStackParamList } from '@/types/types';
import { useNavigation } from '@react-navigation/native';

type PaymentScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "MyHistory">;

const MyHistory: React.FC = ({ route }) => {
    const navigation = useNavigation<PaymentScreenNavigationProp>();

    const fakeBookings = [
        {
            playfieldName: 'Stadium A',
            invoiceNumber: 'INV001',
            address: '123 Main St, City',
            time: '10:00 AM - 12:00 PM',
            price: 100000,
            status: 'Used',
        },
        {
            playfieldName: 'Stadium B',
            invoiceNumber: 'INV002',
            address: '456 Another St, City',
            time: '01:00 PM - 03:00 PM',
            price: 200000,
            status: 'Cancel',
        },
    ];
    const allBookings = [...fakeBookings];

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
                <Text>Time: {item.time}</Text>
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
