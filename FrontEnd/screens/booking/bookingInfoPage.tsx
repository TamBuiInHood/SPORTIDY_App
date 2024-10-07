import React, { useRef, useState } from 'react';
import { View, Text, Image, Dimensions, TouchableOpacity, StyleSheet, ScrollView, Alert, Linking } from 'react-native';
import DateTimePicker from '@react-native-community/datetimepicker';
import DropDownPicker from 'react-native-dropdown-picker';
import ProgressBar from '@/components/ProgressBar';
import { RadioButton } from 'react-native-paper';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import axios from 'axios';
import { RootStackParamList } from '@/types/types';
import { Ionicons } from '@expo/vector-icons';
import QRCode from 'react-native-qrcode-svg';

type PaymentScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "BookingInformationPage">;

const { width } = Dimensions.get('window');

const BookingInformationPage: React.FC = () => {
  const [selectedPaymentMethod, setSelectedPaymentMethod] = useState('Transfer');
  const [date, setDate] = useState(new Date());
  const [selectedTime, setSelectedTime] = useState('1');
  const [openTimeDropdown, setOpenTimeDropdown] = useState(false);
  const [bookingCode, setBookingCode] = useState<string | null>(null); // Initialize bookingCode in state
  const qrCodeRef = useRef<QRCode | null>(null);
  const totalPrice = 120000 * parseInt(selectedTime);
  const navigation = useNavigation<PaymentScreenNavigationProp>();

  const timeOptions = [
    { label: '1h', value: '1' },
    { label: '2h', value: '2' },
    { label: '3h', value: '3' },
  ];

  const onChangeDate = (event: any, selectedDate?: Date) => {
    const currentDate = selectedDate || date;
    setDate(currentDate);
  };

  const handlePlaceOrder = async () => {
    const generatedBookingCode = `BOOK-${new Date().getTime()}`;
    setBookingCode(generatedBookingCode); // Set bookingCode to state
   
    const qrCodeData = await new Promise<string>((resolve, reject) => {
      qrCodeRef.current?.toDataURL((dataURL: any) => {
        if (dataURL) {
          resolve(dataURL); // QR code as a base64 string
        } else {
          reject(new Error('Failed to generate QR code'));
        }
      });
    });
    try {
      // Step 1: Create booking
      const bookingData = {
        price: totalPrice, // Pass the price
        dateStart: date.toISOString(), // Convert start date to ISO string
        dateEnd: new Date(date.getTime() + parseInt(selectedTime) * 60 * 60 * 1000).toISOString(), // Calculate and pass end date
        barCode: qrCodeData, // Ensure to provide a valid barcode
        playFieldId: 1, // Ensure playFieldId is a valid positive number
        customerId: 1, // Provide valid customer ID
      };

      const bookingResponse = await axios.post(
        'https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/bookings',
        bookingData
      );

      console.log('Booking API Response:', bookingResponse.data);

      // Step 2: Get booking by booking ID
      const bookingId = bookingResponse.data.id;
      const getBookingResponse = await axios.get(
        `https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/bookings/${bookingId}`
      );

      console.log('Get Booking API Response:', getBookingResponse.data);

      // Step 3: Create payment link
      const paymentData = {
        bookingCode: bookingCode,
        amount: totalPrice,
        description: "Booking Football Field",
        buyerName: "John Doe",
        buyerPhone: "0123456789",
        userId: "user123",
        playfieldName: "Football playfields",
        playfieldId: 1,
        hour: parseInt(selectedTime),
      };

      const paymentResponse = await axios.post(
        'https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/payment/create-payment-link',
        paymentData
      );

      console.log('Payment API Response:', paymentResponse.data);

      // Step 4: Check and open payment link
      if (paymentResponse.data?.data?.checkoutUrl) {
        const checkoutUrl = paymentResponse.data.data.checkoutUrl;

        // Open payment link in the default browser
        await Linking.openURL(checkoutUrl);

        // Step 5: Navigate to PaymentBookingPage with booking details
        navigation.navigate('PaymentBooking', {
          bookingCode: getBookingResponse.data.bookingCode,
          totalPrice: totalPrice,
          dateStart: date.toISOString(),
          dateEnd: new Date(date.getTime() + parseInt(selectedTime) * 60 * 60 * 1000).toISOString(),
          playfieldName: "Football playfields",
          location: "30 Tháng 4, Phú Thọ, Thủ Dầu Một, Bình Dương",
          time: selectedTime,
        });
      } else {
        Alert.alert('Error', 'Failed to retrieve payment URL.');
      }
    } catch (error: any) {
      console.error('Error during booking or payment:', error.response?.data || error.message);
      Alert.alert('Error', 'Failed to process your request. Please try again.');
    }
  };
  return (
    <ScrollView style={styles.container}>
      <Text style={styles.title}>Booking Information</Text>
      <ProgressBar currentStep={2} />
      <Image source={{ uri: 'https://i.pinimg.com/564x/c0/1d/86/c01d86793f036692c821472575d16809.jpg' }} style={styles.fieldImage} />
      <Text style={styles.fieldName}>Football playfields</Text>
      <Text style={styles.price}>120,000đ / hour</Text>
      <Text style={styles.location}>Location: 30 Tháng 4, Phú Thọ, Thủ Dầu Một, Bình Dương</Text>

      <View style={styles.inputContainer}>
        <View style={styles.row}>
          <Ionicons name='card-outline' size={25} style={styles.icon}></Ionicons>
          <Text style={styles.label}>
            Payment Method</Text>
        </View>
        <View style={styles.radioGroup}>

          <Text style={styles.radioLabel}>Pay by Transfer</Text>
          <Ionicons name='checkbox-sharp' size={25} color={'#ffd591'} />
        </View>
      </View>

      <View style={styles.inputContainer}>
        <View style={styles.row}>
          <Ionicons name='calendar-outline' size={25} style={styles.icon} />
          <Text style={styles.label}>Date</Text>
        </View>
        <DateTimePicker
          value={date}
          mode="datetime"
          display="default"
          onChange={onChangeDate}
          style={styles.datePicker}
        />
      </View>

      <View style={styles.inputContainer}>
        <View style={styles.row}>
          <Ionicons name='time-outline' size={25} style={styles.icon} />
          <Text style={styles.label}>Time</Text>
        </View>
        <DropDownPicker
          open={openTimeDropdown}
          value={selectedTime}
          items={timeOptions}
          setOpen={setOpenTimeDropdown}
          setValue={setSelectedTime}
          style={styles.dropdown}
          containerStyle={styles.dropdownContainer}
        />
      </View>

      <Text style={styles.totalPrice}>Total price: {totalPrice.toLocaleString()} VND</Text>

      <TouchableOpacity style={styles.orderButton} onPress={handlePlaceOrder}>
        <Text style={styles.orderButtonText}>Place Order</Text>
      </TouchableOpacity>
      {bookingCode ? (
        <QRCode
          value={bookingCode}
          getRef={(ref) => (qrCodeRef.current = ref)}  // Store the reference to QRCode component
        />
      ) : (
        <Text style={styles.qrPlaceholder}>QR code will be generated after placing the order.</Text>
      )}
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#fff',
  },
  title: {
    textAlign: 'center',
    fontSize: 22,
    fontWeight: 'bold',
    marginTop: 30,
    backgroundColor: '#ff951d',
    padding: 10,
    color: '#ffff'
  },
  fieldImage: {
    width: '100%',
    height: width * 0.6,
    borderRadius: 10,
    marginVertical: 20,
  },
  fieldName: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 5,
    color: '#F58400',
    textAlign: 'center',
  },
  price: {
    fontSize: 16,
    color: '#F58400',
    marginBottom: 10,
    textAlign: 'center',
  },
  location: {
    fontSize: 14,
    color: 'gray',
    marginBottom: 20,
    textAlign: 'center',
  },
  inputContainer: {
    marginBottom: 15,
  },
  label: {
    fontSize: 16,
    marginBottom: 5,
    fontWeight: '500',

  },
  dropdown: {
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 5,
    marginVertical: 10,
  },
  dropdownContainer: {
    height: 50,
    width: '100%',
  },
  totalPrice: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 20,
    textAlign: 'center',
  },
  orderButton: {
    backgroundColor: '#f39c12',
    padding: 15,
    borderRadius: 10,
    alignItems: 'center',
    marginBottom: 80
  },
  orderButtonText: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
  },
  radioGroup: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  radioLabel: {
    marginLeft: 10,
    fontSize: 16,
  },
  row: {
    flexDirection: 'row', // Align items horizontally
    alignItems: 'center',
  },
  icon: {
    marginRight: 10, // Adjust space between icon and text
  },
  datePicker: {
    marginRight: 120,
    marginTop: 5,
    color: 'white'
  },
});

export default BookingInformationPage;
