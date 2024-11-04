import { View, Text, TouchableOpacity, FlatList, StyleSheet, TextInput } from 'react-native';
import React, { useState, useEffect } from 'react';
import { Card } from 'react-native-paper';
import { LinearGradient } from 'expo-linear-gradient';
import { useNavigation } from 'expo-router';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { RootStackParamList } from '@/types/types';

interface Booking {
  bookingId: string;
  bookingCode: string;
  playField: {
    playFieldName: string;
    address: string;
  };
  dateStart: string;
  dateEnd: string;
  price: string;
  status: number;
}

type HomeScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "HomeScreen">;

const History = () => {
  const [bookings, setBookings] = useState<Booking[]>([]);
  const [filteredBookings, setFilteredBookings] = useState<Booking[]>([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState<'All' | 'Not Yet' | 'Used'>('All');
  const navigation = useNavigation<HomeScreenNavigationProp>();

  useEffect(() => {
    const fetchBookings = async () => {
      try {
        const ownerId = await AsyncStorage.getItem('userId');

        if (ownerId) {
          const response = await axios.get(
            `https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/bookings/owner/${ownerId}`
          );

          const bookingsData = response.data.listBooking;
          console.log("Booking", bookingsData);

          if (Array.isArray(bookingsData)) {
            setBookings(bookingsData);
            setFilteredBookings(bookingsData);
          } else {
            console.warn('Expected an array for listBooking, but received:', bookingsData);
          }
        }
      } catch (error) {
        console.error('Error fetching bookings:', error);
      }
    };
    fetchBookings();
  }, []);
  const handleConfirm = async (bookingCode: string) => {
    try {
      const response = await axios.put(
        `https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/booking/update-status`,
        {
          bookingCode: bookingCode,
          status: 2
        } // Update status to '3'
      );

      if (response.status === 200) {
        // Update the local bookings state
        setBookings(prevBookings =>
          prevBookings.map(booking =>
            booking.bookingId === bookingId ? { ...booking, status: 3 } : booking
          )
        );
      }
    } catch (error) {
      console.error('Error updating booking status:', error);
    }
  };
  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-based
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');

    return `${year}-${month}-${day} ${hours}:${minutes}`;
  };

  useEffect(() => {
    let updatedBookings = bookings;

    // Filter by playFieldName
    if (searchTerm) {
      updatedBookings = updatedBookings.filter((booking) =>
        booking.playField.playFieldName.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }

    // Filter by status
    if (statusFilter !== 'All') {
      updatedBookings = updatedBookings.filter((booking) =>
        (statusFilter === 'Used' ? booking.status === 3 : booking.status === 1)
      );
    }

    setFilteredBookings(updatedBookings);
  }, [searchTerm, statusFilter, bookings]);


  const renderBooking = ({ item }: { item: Booking }) => {
    const statusColor = item.status === 3 ? '#4CAF50' : '#E74C3C'; // Green for "Used", red for "Not Yet"
    const handleViewDetails = () => {
      navigation.navigate('BookingDetail', { bookingId: item.bookingId });
    };

    return (
      <Card style={styles.card}>
        <View style={[styles.statusHeader, { backgroundColor: statusColor }]}>
          <Text style={styles.fieldText}>{item.status === 3? 'Used' : 'Not Yet'}</Text>
        </View>
        <View style={styles.cardContent}>
        <Text style={styles.playFieldName}>PlayField: {item.playField.playFieldName}</Text>
          <Text style={styles.label}>Check-in: {formatDate(item.dateStart)}</Text>
          <Text style={styles.label}>Check-out: {formatDate(item.dateEnd)}</Text>
          <Text style={styles.label}>Price: {item.price}</Text>
          <Text style={styles.label}>Address: {item.playField.address}</Text>
          {item.status === 1 ? (
            <TouchableOpacity onPress={() => handleConfirm(item.bookingCode)}>
              <Text style={styles.manageText}>Confirm</Text>
            </TouchableOpacity>
          ) : (
            null
          )}
        </View>
      </Card>
    );
  };

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <LinearGradient colors={["#76B852", "#A0B853"]} style={styles.gradient}>
          <Text style={styles.headerTitle}>Your Booking's PlayFields</Text>
        </LinearGradient>
      </View>
      <View style={styles.searchContainer}>
        <TextInput
          style={styles.searchInput}
          placeholder="Search by play field name"
          value={searchTerm}
          onChangeText={setSearchTerm}
        />
      </View>
      <View style={styles.filterContainer}>
        {['All', 'Not Yet', 'Used'].map((status) => (
          <TouchableOpacity
            key={status}
            style={[styles.filterButton, statusFilter === status && styles.activeFilter]}
            onPress={() => setStatusFilter(status as 'All' | 'Not Yet' | 'Used')}
          >
            <Text style={styles.filterText}>{status}</Text>
          </TouchableOpacity>
        ))}
      </View>
      <FlatList
        data={filteredBookings}
        renderItem={renderBooking}
        keyExtractor={(item) => item.bookingId.toString()}
        contentContainerStyle={styles.listContent}
        showsHorizontalScrollIndicator={false}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f5f5f5',
    paddingTop: 50,
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
  },
  headerTitle: {
    fontSize: 22,
    fontWeight: 'bold',
    color: '#fff',
    paddingVertical: 25,
  },
  gradient: {
    width: '100%',
    alignItems: 'center',
    paddingBottom: 20,
  },
  searchContainer: {
    padding: 15,
    paddingBottom: 5,
  },
  searchInput: {
    height: 45,
    borderColor: '#bbb',
    borderWidth: 1,
    borderRadius: 25,
    paddingHorizontal: 15,
    backgroundColor: '#fff',
  },
  filterContainer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    paddingVertical: 15,
  },
  filterButton: {
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderRadius: 20,
    backgroundColor: '#ddd',
  },
  activeFilter: {
    backgroundColor: '#76B852',
  },
  filterText: {
    color: '#333',
    fontWeight: 'bold',
  },
  listContent: {
    padding: 50,
    alignItems: 'center',
  },
  card: {
    marginVertical: 8,
    borderRadius: 12,
    elevation: 4,
    width: 320,
    marginHorizontal: 10,
    backgroundColor: '#fff',
  },
  statusHeader: {
    padding: 12,
    borderTopLeftRadius: 12,
    borderTopRightRadius: 12,
  },
  fieldText: {
    fontSize: 16,
    color: '#fff',
    fontWeight: '600',
  },
  cardContent: {
    padding: 15,
  },
  label: {
    fontSize: 14,
    color: '#555',
    marginBottom: 6,
  },
  manageText: {
    fontSize: 14,
    color: '#007BFF',
    marginTop: 10,
    fontWeight: 'bold',
  },
  playFieldName: {
    fontWeight: 'bold'
  }
});

export default History;
