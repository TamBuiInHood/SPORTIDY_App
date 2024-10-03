import React, { useEffect } from 'react';
import { View, Text, StyleSheet, Image, TouchableOpacity, Alert } from 'react-native';
import SvgQRCode from 'react-native-qrcode-svg';
import axios from 'axios';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RootStackParamList } from '@/types/types';

type PaymentScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "PaymentBooking">;

const PaymentSuccessPage: React.FC = () => {
  const navigation = useNavigation<PaymentScreenNavigationProp>();
  const [paymentLink, setPaymentLink] = React.useState<string | null>(null);

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

  // Call the API to create the payment link
  const createPaymentLink = async () => {
    try {
      const response = await axios.post('https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/payment/create-payment-link', {
        invoiceNumber: orderDetails.invoiceNumber,
        amount: 1000000,  // Amount in VND, based on your order
        paymentMethod: orderDetails.paymentMethod,
      });

      const paymentData = response.data;
      if (paymentData && paymentData.link) {
        setPaymentLink(paymentData.link);  // Store payment link for QR or further action
      } else {
        Alert.alert('Error', 'Unable to get payment link');
      }
    } catch (error) {
      console.error('Error creating payment link:', error);
      Alert.alert('Error', 'Something went wrong while creating the payment link');
    }
  };

  useEffect(() => {
    createPaymentLink();
  }, []);

  const orderDetailsString = JSON.stringify(orderDetails);

  return (
    <View style={styles.container}>
      <Image
        source={{ uri: 'https://i.pinimg.com/originals/a2/f4/70/a2f4707d5c9f54e1d67be007d25ff3a4.png' }}
        style={styles.checkmark}
      />
      <Text style={styles.successText}>Your payment was successful!</Text>

      <View style={styles.detailsContainer}>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Invoice Number:</Text>
          <Text style={styles.value}>{orderDetails.invoiceNumber}</Text>
        </View>
        {/* More details */}
        <View style={styles.detailRow}>
          <Text style={styles.label}>Amount Paid:</Text>
          <Text style={styles.value}>{orderDetails.amountPaid}</Text>
        </View>
        <View style={styles.detailRow}>
          <Text style={styles.label}>Status:</Text>
          <Text style={styles.value}>{orderDetails.status}</Text>
        </View>
      </View>

      {/* Display the payment link or QR code */}
      {paymentLink ? (
        <>
          <SvgQRCode value={paymentLink} size={150} />
          <TouchableOpacity
            style={styles.returnButton}
            onPress={() => navigation.navigate('HomeScreen')}>
            <Text style={styles.returnButtonText}>Return to Home</Text>
          </TouchableOpacity>
        </>
      ) : (
        <Text>Loading payment link...</Text>
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#fff',
    alignItems: 'center',
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
