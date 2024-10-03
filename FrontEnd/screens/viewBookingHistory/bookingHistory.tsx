import React from 'react';
import { View, Text, FlatList, StyleSheet } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const MyHistory: React.FC = ({ route }) => {
  const { bookings } = route.params;

  // Render each booking item
  const renderBookingItem = ({ item }) => {
    let statusColor;
    if (item.status === 'Used') {
      statusColor = 'green';
    } else if (item.status === 'Cancel') {
      statusColor = 'red';
    } else {
      statusColor = '#5991FF';
    }

    return (
      <View style={styles.bookingItem}>
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
      </View>
    );
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>My Booking History</Text>
      <FlatList
        data={bookings}
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
    color: '#ffff'
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
