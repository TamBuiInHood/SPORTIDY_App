import React from 'react';
import { Text, View, StyleSheet, Button, Alert } from 'react-native';
import { RouteProp } from '@react-navigation/native';

type QRcodeDetailProps = {
  route: RouteProp<{ params: { bookingDetails: any } }, 'params'>; // Define route prop types
};

const QRcodeDetail: React.FC<QRcodeDetailProps> = ({ route }) => {
  const { bookingDetails } = route.params;
  console.log("Received booking details:", bookingDetails);

  // Function to simulate updating the booking status
  const updateBookingStatus = () => {
    // Show an alert indicating the status was updated
    Alert.alert('Success', 'Booking status updated to "Accepted".');
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Booking Details</Text>
      <Text>Invoice Number: {bookingDetails.invoiceNumber || 'N/A'}</Text>
      <Text>Amount Paid: {bookingDetails.amountPaid || 'N/A'}</Text>
      <Text>Status: {bookingDetails.status || 'N/A'}</Text>
      <Text>Location: {bookingDetails.address || 'N/A'}</Text>
      <Text>Date: {bookingDetails.dateStart || 'N/A'} - {bookingDetails.dateEnd || 'N/A'}</Text>

      {/* Show the Accept button */}
      <Button title="Accept" onPress={updateBookingStatus} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    justifyContent: 'center',
    alignItems: 'center',
  },
  title: {
    fontSize: 22,
    fontWeight: 'bold',
    marginBottom: 20,
  },
});

export default QRcodeDetail;
