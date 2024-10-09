import React, { useRef, useState } from 'react';
import { View, Text, Image, Dimensions, TouchableOpacity, StyleSheet, ScrollView, Alert, Linking } from 'react-native';
import DateTimePicker from '@react-native-community/datetimepicker';
import DropDownPicker from 'react-native-dropdown-picker';
import ProgressBar from '@/components/ProgressBar';
import { RadioButton } from 'react-native-paper';
import { useNavigation, useRoute } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import axios from 'axios';
import { RootStackParamList } from '@/types/types';
import { Ionicons } from '@expo/vector-icons';
import QRCode from 'react-native-qrcode-svg';
type PaymentScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "BookingInformationPage">;

const { width } = Dimensions.get('window');

const BookingInformationPage: React.FC = () => {
  const route = useRoute();
  const [selectedPaymentMethod, setSelectedPaymentMethod] = useState('Transfer');
  const [date, setDate] = useState(new Date());
  const [selectedTime, setSelectedTime] = useState('1');
  const [openTimeDropdown, setOpenTimeDropdown] = useState(false);
  const [bookingCode, setBookingCode] = useState<string | null>(null); // Initialize bookingCode in state
  const qrCodeRef = useRef<QRCode | null>(null);
  const totalPrice = 120000 * parseInt(selectedTime);
  const navigation = useNavigation<PaymentScreenNavigationProp>();
  const { playfieldName, address, image, price } = route.params;  
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
    try {
      // Step 1: Generate a booking code
      const randomCode = Math.floor(100000000 + Math.random() * 900000000).toString();
      setBookingCode(randomCode);
  
      // Wait for bookingCode to update in state
      if (randomCode) {
        // Step 2: Create payment link
        const paymentData = {
          bookingCode: randomCode, // Use the generated randomCode
          amount: price,
          description: "Booking Football Field",
          buyerName: "User",
          buyerPhone: "0123456789",
          userId: "user123",
          playfieldName,
          playfieldId: 1,
          address,
          hour: parseInt(selectedTime),
        };
  
        const paymentResponse = await axios.post(
          'https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/payment/create-payment-link',
          paymentData
        );
  
        console.log('Payment API Response:', paymentResponse.data);
  
        // Step 3: Check and open payment link
        if (paymentResponse.data?.data?.checkoutUrl) {
          const checkoutUrl = paymentResponse.data.data.checkoutUrl;
  
          // Open payment link in the default browser
          await Linking.openURL(checkoutUrl);
  
          // Step 4: Navigate to PaymentBookingPage with booking details
          navigation.navigate('PaymentBooking', {
            bookingCode: randomCode, // Pass the correct booking code
            totalPrice: price,
            dateStart: date.toISOString(),
            dateEnd: new Date(date.getTime() + parseInt(selectedTime) * 60 * 60 * 1000).toISOString(),
            playfieldName ,// Pass here
            location: address,
            time: selectedTime,
          });
        } else {
          Alert.alert('Error', 'Failed to retrieve payment URL.');
        }
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
      <Image source={{uri: image}} style={styles.fieldImage} />
      <Text style={styles.fieldName}>{playfieldName}</Text>
      <Text style={styles.price}>{price}/hours</Text>
      <Text style={styles.location}>{address}</Text>

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

      <Text style={styles.totalPrice}>Total price: {price.toLocaleString()} VND</Text>

      <TouchableOpacity style={styles.orderButton} onPress={handlePlaceOrder}>
        <Text style={styles.orderButtonText}>Place Order</Text>
      </TouchableOpacity>
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
