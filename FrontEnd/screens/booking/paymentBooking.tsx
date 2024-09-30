import React from 'react';
import { View, Text, StyleSheet, Image, TouchableOpacity } from 'react-native';
import SvgQRCode from 'react-native-qrcode-svg';
import { useNavigation } from '@react-navigation/native';

const PaymentSuccessPage: React.FC = () => {
  const navigation = useNavigation();

  const orderDetails = {
    invoiceNumber: "INV567488240UI",
    paymentMethod: "Chuyển khoản",
    paymentDate: "08 June 2024",
    paymentTime: "09:41 AM",
    amountPaid: "1,000,000 VND",
    status: "Successful",
    playfieldName: "Anh Phuoc Star Football Field",
    address: "30 Tháng 4, Phú Thọ, Thủ Dầu Một, Bình Dương",
    timeSlot: "17:00 - 21:00",
    price: "1,000,000 VND",
  };

  const orderDetailsString = JSON.stringify(orderDetails);

  return (
    <View style={styles.container}>
      <Image 
        source={{ uri: 'https://i.pinimg.com/originals/a2/f4/70/a2f4707d5c9f54e1d67be007d25ff3a4.png' }} 
        style={styles.checkmark} 
      />
      <Text style={styles.successText}>Your payment was successful!</Text>
      
      {/* Payment Details */}
      <View style={styles.detailsContainer}>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Invoice Number:</Text>
          <Text style={styles.value}>{orderDetails.invoiceNumber}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Payment Method:</Text>
          <Text style={styles.value}>{orderDetails.paymentMethod}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Date:</Text>
          <Text style={styles.value}>{orderDetails.paymentDate}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Time:</Text>
          <Text style={styles.value}>{orderDetails.paymentTime}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Amount Paid:</Text>
          <Text style={styles.value}>{orderDetails.amountPaid}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Status:</Text>
          <Text style={styles.value}>{orderDetails.status}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Playfield Name:</Text>
          <Text style={styles.value}>{orderDetails.playfieldName}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Address:</Text>
          <Text style={styles.value}>{orderDetails.address}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Time:</Text>
          <Text style={styles.value}>{orderDetails.timeSlot}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Price:</Text>
          <Text style={styles.value}>{orderDetails.price}</Text>
        </View>
      </View>

      <SvgQRCode value={orderDetailsString} size={150} />

      <TouchableOpacity style={styles.returnButton} >
        <Text style={styles.returnButtonText}>Return to Home</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#fff',
    alignItems: 'center',
    borderWidth: 1,
    shadowColor: 'grey'
  },
  checkmark: {
    width: 100,
    height: 100,
    marginVertical: 20,
  },
  successText: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#4CAF50',
    marginBottom: 20,
  },
  detailsContainer: {
    width: '100%',
    marginBottom: 20,
  },
  detailRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 10,
  },
  label: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  value: {
    fontSize: 16,
  },
  returnButton: {
    backgroundColor: '#f39c12',
    padding: 15,
    borderRadius: 10,
    alignItems: 'center',
    width: '100%',
    marginTop: 20,
  },
  returnButtonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default PaymentSuccessPage;
