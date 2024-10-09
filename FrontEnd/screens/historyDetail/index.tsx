import { View, Text, Button, StyleSheet } from 'react-native';
import React, { useState, useEffect } from 'react';
import { useRoute, useNavigation } from '@react-navigation/native';

interface Booking {
  id: string;
  field: string;
  checkIn: string;
  checkOut: string;
  price: string;
  customer: string;
  phone: string;
  status: 'Success' | 'Cancel' | 'Accept';
}

// Mock bookings for testing
const initialBookings: Booking[] = [
  {
    id: '67899',
    field: 'Field 1',
    checkIn: '15:00',
    checkOut: '16:00',
    price: '100,000 VND',
    customer: 'Nguyen Van B',
    phone: '0123456789',
    status: 'Success',
  },
  {
    id: '67899',
    field: 'Field 2',
    checkIn: '15:00',
    checkOut: '16:00',
    price: '100,000 VND',
    customer: 'Nguyen Van B',
    phone: '0123456789',
    status: 'Cancel',
  },
  {
    id: '12325',
    field: 'Field 3',
    checkIn: '15:00',
    checkOut: '16:00',
    price: '100,000 VND',
    customer: 'Nguyen Van C',
    phone: '0123456789',
    status: 'Success',
  },
];

const BookingDetail = () => {
  const [booking, setBooking] = useState<Booking | null>(null);
  const route = useRoute();
  const navigation = useNavigation();

  // Check if bookingId exists in params
  const bookingId = route.params?.bookingId;

  useEffect(() => {
    if (bookingId) {
      const fetchBookingDetail = () => {
        const bookingDetails = initialBookings.find(b => b.id === bookingId);
        if (bookingDetails) {
          setBooking(bookingDetails);
        } else {
          console.error('Booking not found');
        }
      };
      fetchBookingDetail();
    }
  }, [bookingId]);

  const handleAcceptBooking = () => {
    if (booking) {
      const updatedBooking = { ...booking, status: 'Accept' };
      setBooking(updatedBooking);
      alert('Booking status updated to Accept!');
    }
  };

  return (
    <View style={styles.container}>
      {booking ? (
        <>
          <Text style={styles.header}>Booking Detail</Text>
          <Text style={styles.label}>Booking ID: {booking.id}</Text>
          <Text style={styles.label}>Field: {booking.field}</Text>
          <Text style={styles.label}>Check-in: {booking.checkIn}</Text>
          <Text style={styles.label}>Check-out: {booking.checkOut}</Text>
          <Text style={styles.label}>Price: {booking.price}</Text>
          <Text style={styles.label}>Customer: {booking.customer}</Text>
          <Text style={styles.label}>Phone: {booking.phone}</Text>
          <Text style={styles.label}>Status: {booking.status}</Text>

          {booking.status === 'Success' && (
            <Button title="Accept Booking" onPress={handleAcceptBooking} />
          )}
        </>
      ) : (
        <Text>Loading booking details...</Text>
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
  },
  header: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
  },
  label: {
    fontSize: 16,
    marginBottom: 10,
  },
});

export default BookingDetail;
