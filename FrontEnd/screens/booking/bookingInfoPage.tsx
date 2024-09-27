import React, { useState } from 'react';
import { View, Text, Image, Dimensions, TouchableOpacity, StyleSheet, ScrollView } from 'react-native';
import DateTimePicker from '@react-native-community/datetimepicker';
import DropDownPicker from 'react-native-dropdown-picker';
import ProgressBar from '@/components/ProgressBar';
import { RadioButton } from 'react-native-paper';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';

const { width } = Dimensions.get('window');

const BookingInformationPage: React.FC = () => {
  const [selectedPaymentMethod, setSelectedPaymentMethod] = useState('Transfer');
  const [date, setDate] = useState(new Date());
  const [selectedTime, setSelectedTime] = useState('1h');
  const [openPaymentDropdown, setOpenPaymentDropdown] = useState(false);
  const [openTimeDropdown, setOpenTimeDropdown] = useState(false);

  const totalPrice = 120000 * parseInt(selectedTime); 
  const navigation = useNavigation<NativeStackNavigationProp<any>>();


  const timeOptions = [
    { label: '1h', value: '1' },
    { label: '2h', value: '2' },
    { label: '3h', value: '3' },
  ];

  const onChangeDate = (event: any, selectedDate?: Date) => {
    const currentDate = selectedDate || date;
    setDate(currentDate);
  };

  return (
    <ScrollView style={styles.container}>
      <Text style={styles.title}>Booking Information</Text>
      <ProgressBar currentStep={2} />
      <Image source={{ uri: 'https://i.pinimg.com/564x/c0/1d/86/c01d86793f036692c821472575d16809.jpg' }} style={styles.fieldImage} />
      <Text style={styles.fieldName}>Football playfields</Text>
      <Text style={styles.price}>120,000đ / hour</Text>
      <Text style={styles.location}>Location: 30 Tháng 4, Phú Thọ, Thủ Dầu Một, Bình Dương</Text>

      {/* Payment Method Dropdown */}
      <View style={styles.inputContainer}>
        <Text style={styles.label}>Payment Method</Text>
        <View style={styles.radioGroup}>
          <RadioButton
            value="Transfer"
            status={selectedPaymentMethod === 'Transfer' ? 'checked' : 'unchecked'}
            onPress={() => setSelectedPaymentMethod('Transfer')}
          />
          <Text style={styles.radioLabel}>Chuyển khoản (Pay by Transfer)</Text>
        </View>
      </View>

      {/* Date Picker */}
      <View style={styles.inputContainer}>
        <Text style={styles.label}>Date</Text>
        <DateTimePicker
          value={date}
          mode="date"
          display="default"
          onChange={onChangeDate}
        />
      </View>

      <View style={styles.inputContainer}>
        <Text style={styles.label}>Time</Text>
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

      <TouchableOpacity style={styles.orderButton} onPress={() => navigation.navigate('(routes)/paymentBooking')}>
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
});

export default BookingInformationPage;
