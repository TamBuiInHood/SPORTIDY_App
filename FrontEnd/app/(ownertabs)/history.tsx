import { View, Text, TouchableOpacity, FlatList, StyleSheet } from 'react-native'
import React from 'react'
import { Card } from 'react-native-paper';
import { Ionicons } from '@expo/vector-icons';
import { LinearGradient } from 'expo-linear-gradient';
interface Booking {
  id: string;
  field: string;
  checkIn: string;
  checkOut: string;
  price: string;
  customer: string;
  phone: string;
  status: 'Success' | 'Cancel';
}

const bookings: Booking[] = [
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
const history = () => {
  const renderBooking = ({ item }: { item: Booking }) => {
    const statusColor = item.status === 'Success' ? '#4CAF50' : '#E74C3C';

    return (
      <Card style={styles.card}>
        <View style={[styles.statusHeader, { backgroundColor: statusColor }]}>
          <Text style={styles.fieldText}>{item.field}</Text>
          <Text style={styles.statusText}>{item.status}</Text>
        </View>
        <View style={styles.cardContent}>
          <Text style={styles.label}>Booking ID: {item.id}</Text>
          <Text style={styles.label}>
            Check-in & Check-out: {item.checkIn} - {item.checkOut}
          </Text>
          <Text style={styles.label}>Price: {item.price}</Text>
          <Text style={styles.label}>Name customer: {item.customer}</Text>
          <Text style={styles.label}>Phone: {item.phone}</Text>
          {item.status === 'Success' && (
            <TouchableOpacity>
              <Text style={styles.manageText}>Manage</Text>
            </TouchableOpacity>
          )}
        </View>
      </Card>
    );
  };
  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <LinearGradient colors={["#76B852", "#A0B853"]} style={styles.gradient}>
          <Text style={styles.headerTitle}>Your PlayFields</Text>
        </LinearGradient>
      </View>
       <FlatList
      data={bookings}
      renderItem={renderBooking}
      keyExtractor={(item) => item.id}
      contentContainerStyle={styles.listContent}
    />
    </View>
   
  );
};
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    paddingTop: 50
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#f9ca24',
    justifyContent: 'center'
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: 'bold',
    color: '#fff',
    paddingVertical: 20
  },
  gradient: {
    width: '100%',
    alignItems: 'center',
  },
  listContent: {
    padding: 10,
  },
  card: {
    marginVertical: 10,
    borderRadius: 10,
    shadowColor: '#000',
    shadowOpacity: 0.1,
    shadowRadius: 10,
    elevation: 3,
  },
  statusHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    padding: 10,
    borderTopLeftRadius: 10,
    borderTopRightRadius: 10,
  },
  fieldText: {
    fontSize: 16,
    color: '#fff',
    fontWeight: 'bold',
  },
  statusText: {
    fontSize: 16,
    color: '#fff',
    fontWeight: 'bold',
  },
  cardContent: {
    padding: 10,
  },
  label: {
    fontSize: 14,
    color: '#333',
    marginBottom: 4,
  },
  manageText: {
    fontSize: 14,
    color: '#007BFF',
    marginTop: 8,
  },
});
export default history