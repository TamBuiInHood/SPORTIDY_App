import React, { useState, useEffect } from 'react';
import { View, Text, TextInput, StyleSheet, TouchableOpacity, Image, ScrollView, Alert } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { LinearGradient } from 'expo-linear-gradient';
import * as ImagePicker from 'expo-image-picker';
import { PlayField, RootStackParamList } from '@/types/types';
import { RouteProp, useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
type PlayfieldDetailScreenRouteProp = RouteProp<RootStackParamList, 'PlayFieldDetail'>;
type PlayfieldDetailScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'PlayFieldDetail'>;

interface PlayfieldDetailScreenProps {
  route: PlayfieldDetailScreenRouteProp;
}



const UpdatePlayField = ({ route }: PlayfieldDetailScreenProps) => {
  const [name, setName] = useState('');
  const [currency, setCurrency] = useState('');
  const [address, setAddress] = useState('');
  const [checkIn, setCheckIn] = useState('');
  const [checkOut, setCheckOut] = useState('');
  const [image, setImage] = useState<string | null>(null);
  const { playfield } = route.params;

  // Giả sử bạn có một playfield cụ thể để cập nhật từ API hoặc từ props.
 useEffect(() => {
  const playfieldToEdit = route.params.playfield; // Lấy dữ liệu từ route
  if (playfieldToEdit) {
    setName(playfieldToEdit.name);
    setCurrency(playfieldToEdit.price);
    setAddress(playfieldToEdit.location);
    setCheckIn(playfieldToEdit.openingHours.split(' - ')[0]);
    setCheckOut(playfieldToEdit.openingHours.split(' - ')[1]);
    setImage(playfieldToEdit.image);
  }
}, [route.params.playfield]); // Chạy một lần khi component mount

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
  const navigation = useNavigation<PlayfieldDetailScreenNavigationProp>();

  const handleSubmit = () => {
    Alert.alert ("Updated playfield!");
    navigation.navigate("(ownertabs)", { params: { screen: "home" } });
    
    const updatedPlayfield = {
      name,
      location: address,
      price: currency,
      image,
      openingHours: `${checkIn} - ${checkOut}`,
      capacity: '40,000 people',
      surface: 'Grass',
      owner: 'Vietnamese government',
      rating: 4.0,
      reviews: 480,
    };

    console.log('Updated playfield:', updatedPlayfield);
  };

  return (
    <ScrollView style={styles.scrollcontainer}>
      <View style={styles.header}>
        <LinearGradient colors={["#76B852", "#A0B853"]} style={styles.gradient}>
          <Text style={styles.headerTitle}>Update PlayField</Text>
        </LinearGradient>
      </View>
      <View style={styles.infor}>
        {/* Hiển thị trường tên */}
        <Text style={styles.title}>Playfield's name</Text>
        <TextInput
          style={styles.input}
          placeholder="My Dinh National stadium"
          value={name}
          onChangeText={setName}
        />

        {/* Hiển thị đơn vị tiền */}
        <Text style={styles.title}>Main currency</Text>
        <TextInput
          style={styles.input}
          placeholder={currency}
          value={currency}
          editable={true}
        />

        {/* Hiển thị địa chỉ */}
        <Text style={styles.title}>Address</Text>
        <TextInput
          style={styles.input}
          placeholder="Nam Từ Liêm, Hà Nội"
          value={address}
          onChangeText={setAddress}
        />

        {/* Hiển thị giờ check-in, check-out */}
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

        {/* Chức năng upload ảnh */}
        <Text style={styles.title}>Photos</Text>
        <View style={styles.uploadContainer}>
          <TouchableOpacity style={styles.uploadButton} onPress={pickImage}>
            <Ionicons name="image-outline" size={24} color="#ff951d" />
            <Text>Add photo</Text>
          </TouchableOpacity>
        </View>
        {image && <Image source={{ uri: image }} style={styles.mediaPreview} />}

        {/* Submit form */}
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
    paddingTop: 50,
    alignItems: 'center',
    justifyContent: 'center',
  },
  headerTitle: {
    fontSize: 24,
    color: '#fff',
    fontWeight: 'bold',
    padding: 10
  },
  gradient: {
    width: '100%',
    alignItems: 'center',
  },
});

export default UpdatePlayField;
