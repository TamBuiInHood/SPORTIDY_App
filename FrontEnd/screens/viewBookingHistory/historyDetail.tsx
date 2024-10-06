import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import QRCode from 'react-native-qrcode-svg'; // Import QRCode component
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RootStackParamList } from '@/types/types';
import { useNavigation } from '@react-navigation/native';
type PaymentScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "HistoryDetail">;

const BookingDetail: React.FC = ({ route }) => {
  const { booking } = route.params; // Get booking details from route params
  const navigation = useNavigation<PaymentScreenNavigationProp>();
  const qrValue = `
  Playfield Name: ${booking.playfieldName}
  Invoice Number: ${booking.invoiceNumber}
  Address: ${booking.address}
  Time: ${booking.time}
  Price: ${booking.price.toLocaleString()} VND
  Status: ${booking.status}
`;
const handleFeedbackPress = () => {
  navigation.navigate('FeedBack',{booking}); // Điều hướng đến FeedbackPage
};
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Booking Detail</Text>

      <View style={styles.detailItem}>
        <Text style={styles.detailLabel}>Playfield Name:</Text>
        <Text style={styles.detailValue}>{booking.playfieldName}</Text>
      </View>

      <View style={styles.detailItem}>
        <Text style={styles.detailLabel}>Invoice Number:</Text>
        <Text style={styles.detailValue}>{booking.invoiceNumber}</Text>
      </View>

      <View style={styles.detailItem}>
        <Text style={styles.detailLabel}>Address:</Text>
        <Text style={styles.detailValue}>{booking.address}</Text>
      </View>

      <View style={styles.detailItem}>
        <Text style={styles.detailLabel}>Time:</Text>
        <Text style={styles.detailValue}>{booking.time}</Text>
      </View>

      <View style={styles.detailItem}>
        <Text style={styles.detailLabel}>Price:</Text>
        <Text style={styles.detailValue}>{booking.price.toLocaleString()} VND</Text>
      </View>

      <View style={styles.detailItem}>
        <Text style={styles.detailLabel}>Status:</Text>
        <Text
          style={[
            styles.status,
            {
              backgroundColor:
                booking.status === 'Used'
                  ? 'green'
                  : booking.status === 'Cancel'
                  ? 'red'
                  : '#5991FF',
            },
          ]}
        >
          {booking.status}
        </Text>
      </View>

      {/* QR Code */}
      <View style={styles.qrCodeContainer}>
        <Text style={styles.qrCodeLabel}>QR Code:</Text>
        <QRCode value={qrValue} size={150} />
      </View>

      <TouchableOpacity style={styles.feedbackButton} onPress={handleFeedbackPress}>
        <Text style={styles.feedbackButtonText}>Feedback Playfield</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 30,
    backgroundColor: '#fff',
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    textAlign: 'center',
    marginVertical: 20,
  },
  detailItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginVertical: 5,
  },
  detailLabel: {
    fontWeight: 'bold',
  },
  detailValue: {
    flex: 1,
    textAlign: 'right',
  },
  status: {
    color: 'white',
    padding: 5,
    borderRadius: 5,
    textAlign: 'center',
    marginTop: 5,
  },
  feedbackContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 20,
  },
  feedbackText: {
    marginRight: 10,
    textAlign: 'center',

  },
  qrCodeContainer: {
    alignItems: 'center',
    marginVertical: 20,
  },
  qrCodeLabel: {
    fontWeight: 'bold',
    marginBottom: 10,
  },
  feedbackButton: {
    backgroundColor: '#FF6347', // Similar to the image background color
    borderRadius: 30, // Adjusts the roundness of the button
    paddingVertical: 15, // Padding for vertical
    paddingHorizontal: 30, // Padding for horizontal
    alignItems: 'center', // Center text horizontally
    marginTop: 20, // Space above the button
  },
  feedbackButtonText: {
    color: '#fff', // White text color
    fontSize: 18, // Font size of the button text
    fontWeight: 'bold', // Bold text
  },
});

export default BookingDetail;
