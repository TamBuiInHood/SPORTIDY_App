import { Ionicons } from '@expo/vector-icons';
import { RouteProp, useRoute } from '@react-navigation/native';
import React, { useEffect, useState } from 'react';
import { View, Text, Image, TouchableOpacity, StyleSheet, Alert, TextInput, ScrollView } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { PlayField, RootStackParamList } from '@/types/types';
import api from '@/config/api';
import DateTimePickerModal from 'react-native-modal-datetime-picker';
import DropDownPicker from 'react-native-dropdown-picker';

type DetailBookingRouteProp = RouteProp<RootStackParamList, 'DetailBookingPage'>;

const DetailBookingPage: React.FC = () => {
  const route = useRoute<DetailBookingRouteProp>();
  const navigation = useNavigation();

  const playId = route.params?.playFieldId;

  const [playfields, setPlayFields] = useState<PlayField | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [selectedTime, setSelectedTime] = useState<string>('1'); // Default duration of 1 hour
  const [dateStart, setDateStart] = useState<Date | null>(null); // Initial state for date start
  const [isDateTimePickerVisible, setDateTimePickerVisible] = useState(false); // State for controlling DateTimePicker visibility
  const [openTimeDropdown, setOpenTimeDropdown] = useState(false);
  const [isDatePickerVisible, setDatePickerVisibility] = useState(false);
  const [userName, setUserName] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [voucherCode, setVoucherCode] = useState<string>('');
  const formattedDate = dateStart ? dateStart.toISOString().slice(0, 10) : 'Select Date';
  const [time, setTime] = useState('');
  const timeOptions = [
    { label: '1h', value: '1' },
    { label: '2h', value: '2' },
    { label: '3h', value: '3' },
  ];

  const formatTime = (time: any) => {
    if (!time) return '--';
    const [hours, minutes] = time.split(':');
    return `${hours.padStart(2, '0')}:${minutes.padStart(2, '0')}`;
  };

  const showDatePicker = () => setDatePickerVisibility(true);
  const hideDatePicker = () => setDatePickerVisibility(false);

  const handleConfirmDate = (selectedDate: any) => {
    const formattedTime = selectedDate.toTimeString().slice(0, 5);
    setTime(formattedTime);
    setDateStart(selectedDate);
    hideDatePicker();
  };

  const totalPrice = playfields?.price * parseInt(selectedTime);

  // Fetch PlayField details
  const DISCOUNT_PERCENT = 20 / 100;
  const discountedPrice = voucherCode === 'PHUNUVIETNAM' ? totalPrice * (1 - DISCOUNT_PERCENT) : totalPrice;
  const priceFormatted = new Intl.NumberFormat('vi-VN', {
    style: 'decimal',
    minimumFractionDigits: 0,
  }).format(discountedPrice);
  useEffect(() => {
    const fetchPlayField = async () => {
      if (!playId) {
        console.error('No playFieldId provided');
        setLoading(false);
        return;
      }

      try {
        const response = await api.getPlayfieldById(playId);
        const result: PlayField = response.data;
        setPlayFields(result);
      } catch (error) {
        console.error('Failed to fetch play field:', error);
        setPlayFields(null);
      } finally {
        setLoading(false); // End loading regardless of success or failure
      }
    };

    fetchPlayField();
  }, [playId]);

  // Handle create booking logic
  const handleCreateBooking = async () => {
    if (!dateStart) {
      Alert.alert('Error', 'Please select a date and time.');
      return;
    }

    // Retrieve customerId from AsyncStorage
    let customerId = await AsyncStorage.getItem('userId');
    if (!customerId) {
      Alert.alert('Error', 'No user found. Please log in again.');
      return;
    }

    const formatDate = (date: Date) => {
      return date.toISOString().slice(0, 19);  // Removes the last 5 characters (e.g., "Z")
    };


    const dateEnd = new Date(dateStart.getTime());
    dateEnd.setHours(dateEnd.getHours() + parseInt(selectedTime)); // assuming selectedTime is the duration in hours
    const dateStartString = formatDate(dateStart);
    const dateEndString = formatDate(dateEnd);
    if (dateEnd <= dateStart) {
      Alert.alert('Error', 'End time must be after start time.');
      return;
    }

    // Calculate price based on duration (price per hour * selected duration)

    const bookingData = {
      playfieldId: playfields?.playFieldId,
      customerId: customerId,
      price: totalPrice, // Calculated price based on duration
      dateStart: dateStartString,
      dateEnd: dateEndString,
      voucher: voucherCode,
    };

    console.log('Booking Data:', bookingData);

    // API call to create booking
    try {
      const response = await fetch('https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/bookings', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(bookingData),
      });

      if (response.ok) {
        const result = await response.json();
        console.log('Booking created successfully:', result);
        const bookingCode = result.data.bookingCode;
        const bookingDetails = result.data;
        navigation.navigate('CheckoutPage', {
          bookingCode: bookingCode,
          bookingDetails: bookingDetails,
          playFieldName: playfields?.playFieldName, // Tên sân chơi
          address: playfields?.address, // Địa chỉ
          userName: userName, // Include userName
          phoneNumber: phoneNumber, // Include phoneNumber
        });
        Alert.alert('Booking created successfully!');
      } else {
        const errorResponse = await response.json();
        console.error('API Error:', errorResponse);
        Alert.alert('Error creating booking', JSON.stringify(errorResponse.errors || 'Unknown error'));
      }
    } catch (error) {
      console.error('Error:', error);
      Alert.alert('Error', 'Something went wrong');
    }
  };

  // Function to show the DateTime picker
  const showDateTimePicker = () => {
    setDateTimePickerVisible(true);
  };

  // Handle date and time selection
  const handleDatePicked = (date: Date) => {
    setDateStart(date);
    setDateTimePickerVisible(false);
  };

  return (
    <ScrollView style={styles.container} contentContainerStyle={{ flexGrow: 1 }}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.backButton} onPress={() => navigation.goBack()}>
          <Text style={styles.backButtonText}>←</Text>
        </TouchableOpacity>

        <View style={styles.headerTitleContainer}>
          <Text style={styles.title}>{playfields?.playFieldName}</Text>
          <Text style={styles.date}>
            {formatTime(playfields?.openTime)} - {formatTime(playfields?.closeTime)}
          </Text>
        </View>

        <View style={styles.buttonContainer}>
          <TouchableOpacity style={styles.button}>
            <Text style={styles.buttonText}>Football pitches</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.button}>
            <Text style={styles.buttonText}>Rugby field</Text>
          </TouchableOpacity>
        </View>
      </View>

      <View style={styles.imageContainer}>
        <Image
          source={{ uri: playfields?.avatarImage }}
          style={styles.image}
        />
      </View>
      <View style={styles.details}>
        <Ionicons name='location-outline' size={20} />
        <Text style={styles.locationText}>{playfields?.address}</Text>
        <View style={styles.inputContainer}>
          <Text style={styles.label}>Enter your name</Text>
          <TextInput
            style={styles.input}
            value={userName}
            onChangeText={setUserName}
            placeholder="Enter your name"
          />
        </View>

        {/* Input for Phone Number */}
        <View style={styles.inputContainer}>
          <Text style={styles.label}>Phone Number</Text>
          <TextInput
            style={styles.input}
            value={phoneNumber}
            onChangeText={setPhoneNumber}
            placeholder="Enter your phone number"
            keyboardType="phone-pad"
          />
        </View>

        <View style={styles.inputContainer}>
          <View style={styles.row}>
            <Text style={styles.label}>Duration</Text>
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

        {/* Date and Time Picker Button */}

        <Text style={styles.label}>Booking Date & Time</Text>

        <TouchableOpacity style={styles.input} onPress={showDatePicker}>
          <Text style={styles.dateText}>{formattedDate} - {time ? time: 'Select Time'}</Text>
          <Ionicons name="calendar-outline" size={24} style={styles.icon} />
        </TouchableOpacity>

        <DateTimePickerModal
          isVisible={isDatePickerVisible}
          mode="datetime"
          onConfirm={handleConfirmDate}
          onCancel={hideDatePicker}
        />
        <View style={styles.inputContainer}>
          <Text style={styles.label}>Voucher Code</Text>
          <TextInput
            style={styles.input}
            value={voucherCode}
            onChangeText={setVoucherCode}
            placeholder="Enter voucher code"
          />
        </View>
        <View>
          <Text style={styles.label}>Total Price: {priceFormatted} VND</Text>
        </View>
      </View>


      <TouchableOpacity style={styles.bookingButton} onPress={handleCreateBooking}>
        <Text style={styles.bookingButtonText}>Booking PlayFields</Text>
      </TouchableOpacity>


    </ScrollView >
  );
};


const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    paddingTop: 30,
    paddingBottom: 100
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 16,
    justifyContent: 'space-between',
  },
  headerTitleContainer: {
    flex: 1,
    marginLeft: 10,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    color: '#F58400'
  },
  date: {
    fontSize: 15,
    color: '#F58400',
    marginTop: 5
  },

  buttonContainer: {
    flexDirection: 'column',
  },
  button: {
    padding: 10,
    borderRadius: 20,
    backgroundColor: '#F58400',
    marginLeft: 10,
    marginTop: 5
  },
  buttonText: {
    color: '#fff',
    fontSize: 16,
    textAlign: 'center'
  },
  backButton: {
    padding: 8,
  },
  backButtonText: {
    fontSize: 20,
    fontWeight: 'bold',
  },

  imageContainer: {
    alignItems: 'center',
    marginVertical: 16,
  },
  image: {
    width: '90%',
    height: 250,
    borderRadius: 20,
  },
  information: {
    padding: 16,
  },

  buttons: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },

  details: {
    padding: 16,
  },
  locationText: {
    fontSize: 16,
    marginBottom: 8,
  },
  pitchStatusText: {
    fontSize: 16,
    fontWeight: 'bold',
    marginVertical: 10,
  },
  pitchOption: {
    padding: 8,
    borderRadius: 8,
    backgroundColor: '#4CAF50',
    marginBottom: 4,
  },
  pitchOptionText: {
    color: '#fff',
    fontSize: 16,
  },
  servicesText: {
    fontSize: 16,
    fontWeight: 'bold',
    marginTop: 8,
  },
  amenitiesText: {
    fontSize: 16,
  },
  bookingButton: {
    padding: 12,
    borderRadius: 50,
    backgroundColor: '#F58400',
    marginHorizontal: 60,
    marginBottom: 100
  },
  bookingButtonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
    textAlign: 'center',
  },
  inputContainer: {
    marginBottom: 16,
  },
  input: {
    height: 50,
    borderColor: '#ddd',
    borderWidth: 1,
    borderRadius: 10,
    paddingHorizontal: 12,
    justifyContent: 'center',
    marginBottom: 16,
  },
  label: {
    fontSize: 16,
    marginBottom: 8,
    color: '#F58400',
    fontWeight: 'bold',
  },
  dateText: {
    fontSize: 16,
    color: '#333',
  },
  icon: {
    position: 'absolute',
    right: 12,
    top: 12,
    color: '#aaa',
  },
});


export default DetailBookingPage;
