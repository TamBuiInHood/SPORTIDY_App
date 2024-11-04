import React, { useState } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, TextInput, ScrollView, Linking, Alert } from 'react-native';
import { useNavigation, useRoute } from '@react-navigation/native';
import axios from 'axios';
import { RootStackParamList } from '@/types/types';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { RadioButton } from 'react-native-paper';  // Assuming you're using this for payment methods
import ProgressBar from '@/components/ProgressBar';
import { format } from 'date-fns';

type CheckoutPageNavigationProp = NativeStackNavigationProp<RootStackParamList, 'CheckoutPage'>;

const CheckoutPage: React.FC = () => {
  const route = useRoute();
  const navigation = useNavigation<CheckoutPageNavigationProp>();

  // Check xem các giá trị từ route.params có truyền đúng không
  console.log('Route Params:', route.params);  // Thêm dòng này

  const { bookingCode, bookingDetails, playFieldName, address, userName, phoneNumber } = route.params;
  console.log('Route Params:', route.params);
  const [selectedPaymentMethod, setSelectedPaymentMethod] = useState<string>('PayOS');
  const [isProcessing, setIsProcessing] = useState(false);
  const dateTimeString = bookingDetails.dateStart;
  const [datePart, timePart] = dateTimeString.split('T');
  const formattedDate = datePart;
  const formattedTime = format(new Date(dateTimeString), 'HH:mm'); const priceFormatted = new Intl.NumberFormat('vi-VN', {
    style: 'decimal',
    minimumFractionDigits: 0,
  }).format(bookingDetails.price);

  const handlePlaceOrder = async () => {
    setIsProcessing(true);
    try {
      const paymentData = {
        bookingCode: bookingCode,
        amount: bookingDetails.price,
        description: 'Booking Football Field',
        playFieldId: bookingDetails.playFiedlId,
        userId: bookingDetails.customerId.toString(),
        playfieldName: playFieldName,
        buyerName: userName,
        buyerPhone: phoneNumber,
        hour: 2
      };
      console.log(paymentData);
      const paymentResponse = await axios.post(
        'https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/payment/create-payment-link',
        paymentData
      );

      if (paymentResponse.data?.data?.checkoutUrl) {
        const checkoutUrl = paymentResponse.data.data.checkoutUrl;
        await Linking.openURL(checkoutUrl);
        const updateStatusData = {
          bookingCode: bookingCode,
          status: 3
        };
        await axios.put(
          'https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/booking/update-status',
          updateStatusData
        );
        navigation.navigate('PaymentBooking', {
          bookingCode: bookingCode,
          amount: bookingDetails.price,
          description: 'Booking Football Field',
          playFieldId: bookingDetails.playFiedlId,
          playfieldName: playFieldName,
          buyerName: userName,
          buyerPhone: phoneNumber,
          dateStart: formattedDate,
          time: formattedTime,
          location: address,
        });
      } else {
        Alert.alert('Error', 'Failed to retrieve payment URL.');
      }
    } catch (error: any) {
      console.error('Payment error:', error.response?.data || error.message);
      Alert.alert('Error', 'Failed to process your payment. Please try again.');
    } finally {
      setIsProcessing(false);
    }
  };

  // Kiểm tra dữ liệu trước khi render
  if (!bookingCode || !bookingDetails) {
    return (
      <View style={styles.container}>
        <Text>No booking details found. Please try again later.</Text>
      </View>
    );
  }

  return (
    <ScrollView style={styles.container}>
      <Text style={styles.header}>Checkout</Text>
      <ProgressBar currentStep={2} />

      {/* Section 1: Booking Summary */}
      <View style={styles.section}>
        <Text style={styles.sectionTitle}>Booking Summary</Text>

        {/* Hiển thị thông tin đặt chỗ */}
        <Text style={styles.infoText}>Play Field: {playFieldName}</Text>
        <Text style={styles.infoText}>Address: {address}</Text>
        <Text style={styles.infoText}>Date: {formattedDate}</Text>
        {/* Total Price */}
        <View style={styles.priceContainer}>
          <Text style={styles.totalPriceLabel}>Total Price:</Text>
          <Text style={styles.totalPrice}> {priceFormatted} VND</Text>
        </View>
      </View>

      {/* Section 2: Payment Methods */}
      <View style={styles.section}>
        <Text style={styles.sectionTitle}>Payment Methods</Text>

        {/* Payment Methods Radio Button */}
        <View style={styles.paymentMethodContainer}>
          <RadioButton
            value="PayOS"
            status={selectedPaymentMethod === 'PayOS' ? 'checked' : 'unchecked'}
            onPress={() => setSelectedPaymentMethod('PayOS')}
          />
          <Text style={styles.paymentMethodText}>PayOS</Text>
        </View>
      </View>

      {/* Place Order Button */}
      <TouchableOpacity style={styles.orderButton} onPress={handlePlaceOrder} disabled={isProcessing}>
        <Text style={styles.orderButtonText}>
          {isProcessing ? 'Processing...' : 'Place Order'}
        </Text>
      </TouchableOpacity>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
    backgroundColor: '#fff',
    paddingTop: 30
  },
  header: {
    fontSize: 24,
    fontWeight: 'bold',
    textAlign: 'center',
    marginVertical: 20,
    color: '#FF9800',
  },
  section: {
    marginVertical: 20,
    padding: 16,
    backgroundColor: '#f9f9f9',
    borderRadius: 10,
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 10,
  },
  input: {
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 8,
    padding: 12,
    marginVertical: 10,
    backgroundColor: '#fff',
  },
  priceContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginTop: 10,
  },
  totalPriceLabel: {
    fontSize: 16,
    color: '#333',
  },
  totalPrice: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#FF9800',
  },
  paymentMethodContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginVertical: 10,
  },
  paymentMethodText: {
    fontSize: 16,
    marginLeft: 10,
  },
  orderButton: {
    backgroundColor: '#FF9800',
    padding: 15,
    borderRadius: 50,
    alignItems: 'center',
    marginTop: 30,
  },
  orderButtonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default CheckoutPage;
