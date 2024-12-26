import React, { useState } from 'react';
import { View, Text, TextInput, StyleSheet, TouchableOpacity, Image, ScrollView } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { LinearGradient } from 'expo-linear-gradient';
import * as ImagePicker from 'expo-image-picker';

const UpdatePlayField = () => {
  const [name, setName] = useState('');
  const [currency, setCurrency] = useState('500,000,000 VND');
  const [address, setAddress] = useState('');
  const [checkIn, setCheckIn] = useState('14:00');
  const [checkOut, setCheckOut] = useState('12:00');
  const [photos, setPhotos] = useState<any[]>([]);
  const [fullName, setFullName] = useState('');
  const [creditCard, setCreditCard] = useState('');
  const [expDate, setExpDate] = useState('');
  const [cvv, setCvv] = useState('');
  const [image, setImage] = useState(null);

  const pickImage = async () => {
    let result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [4, 3],
      quality: 1,
    });
    if (!result.canceled) {
      setImage(result.uri);
    }
  };


  const handleSubmit = () => {
    // Logic to handle the form submission
    console.log({
      name,
      currency,
      address,
      checkIn,
      checkOut,
      photos,
      fullName,
      creditCard,
      expDate,
      cvv,
    });
  };

  return (

    <ScrollView style={styles.scrollcontainer}>
      <View style={styles.header}>
        <LinearGradient colors={["#76B852", "#A0B853"]} style={styles.gradient}>
          <Text style={styles.headerTitle}>Create PlayField</Text>
        </LinearGradient>
      </View>
      <View style={styles.infor}>
        <Text style={styles.title}>Playfield's name</Text>
        <TextInput
          style={styles.input}
          placeholder="My Dinh National stadium"
          value={name}
          onChangeText={setName}
        />

        <Text style={styles.title}>Main currency</Text>
        <TextInput
          style={styles.input}
          placeholder={currency}
          editable={false}
        />

        <Text style={styles.title}>Address</Text>
        <TextInput
          style={styles.input}
          placeholder="Nam Từ Liêm, Hà Nội"
          value={address}
          onChangeText={setAddress}
        />

        <View style={styles.checkinContainer}>
          <View style={styles.checkin}>
            <Text style={styles.title}>Check-in</Text>
            <TextInput
              style={styles.input}
              placeholder="14:00"
              value={checkIn}
              onChangeText={setCheckIn}
            />
          </View>
          <View style={styles.checkout}>
            <Text style={styles.title}>Check-out</Text>
            <TextInput
              style={styles.input}
              placeholder="12:00"
              value={checkOut}
              onChangeText={setCheckOut}
            />
          </View>
        </View>

        <Text style={styles.title}>Photos</Text>
        <View style={styles.uploadContainer}>
          <TouchableOpacity style={styles.uploadButton} onPress={pickImage}>
            <Ionicons name="image-outline" size={24} color="#ff951d" />
            <Text>Add photo</Text>
          </TouchableOpacity>
        </View>
        {image && <Image source={{ uri: image }} style={styles.mediaPreview} />}

        <Text style={styles.title}>Bank Cards</Text>
        <TextInput
          style={styles.input}
          placeholder="Full Name"
          value={fullName}
          onChangeText={setFullName}
        />
        <TextInput
          style={styles.input}
          placeholder="Credit Card Number"
          value={creditCard}
          onChangeText={setCreditCard}
          keyboardType="numeric"
        />
        <View style={styles.expiryContainer}>
          <TextInput
            style={styles.expiryInput}
            placeholder="MM/YY"
            value={expDate}
            onChangeText={setExpDate}
          />
          <TextInput
            style={styles.cvvInput}
            placeholder="CVV"
            value={cvv}
            onChangeText={setCvv}
            keyboardType="numeric"
          />
        </View>
        <TouchableOpacity style={styles.addButton}>
          <Text style={styles.addButtonText}>ADD</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.confirmButton} onPress={handleSubmit}>
          <Text style={styles.confirmButtonText}>CONFIRM</Text>
        </TouchableOpacity>
      </View>
    </ScrollView>

  );
};

const styles = StyleSheet.create({
  scrollcontainer: {
    flex: 1,
    backgroundColor: '#fff',
  },
  infor: {
    padding: 15,
  },
  title: {
    fontSize: 16,
    fontWeight: 'bold',
    marginVertical: 8,
  },
  input: {
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 8,
    padding: 10,
    marginBottom: 16,
  },
  checkinContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 16,
  },
  checkin: {
    width: '48%',
  },
  checkout: {
    width: '48%',
  },
  photoContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
  },
  photo: {
    width: 100,
    height: 100,
    borderRadius: 8,
    marginRight: 8,
  },
  addPhotoButton: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 8,
    padding: 10,
  },
  addPhotoText: {
    marginLeft: 8,
  },
  expiryContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 16,
  },
  expiryInput: {
    width: '48%',
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 8,
    padding: 10,
  },
  cvvInput: {
    width: '48%',
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 8,
    padding: 10,
  },
  addButton: {
    backgroundColor: '#48C9B0',
    borderRadius: 20,
    paddingVertical: 10,
    alignItems: 'center',
    marginBottom: 16,
    marginHorizontal: 140
  },
  addButtonText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },
  paymentMethods: {
    marginBottom: 16,
  },
  confirmButton: {
    backgroundColor: '#48C9B0',
    borderRadius: 8,
    paddingVertical: 10,
    alignItems: 'center',
  },
  confirmButtonText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },
  header: {
    marginTop: 40,
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: 'bold',
    color: '#fff',
    paddingVertical: 40
  },
  gradient: {
    width: '100%',
    alignItems: 'center',
  },
  uploadContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginVertical: 20,
  },
  uploadButton: {
    alignItems: 'center',
    backgroundColor: '#FFF5E0',
    padding: 10,
    borderRadius: 8,
    width: '45%',
  },
  mediaPreview: {
    width: '100%',
    height: 200,
    borderRadius: 10,
    marginTop: 10,
  },
});

export default UpdatePlayField;
