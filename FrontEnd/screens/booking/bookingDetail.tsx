import React from 'react';
import { View, Text, Image, TouchableOpacity, StyleSheet } from 'react-native';

const DetailBookingPage = () => {
  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.backButton}>
          <Text style={styles.backButtonText}>←</Text>
        </TouchableOpacity>
        <Text style={styles.title}>Go Dau Stadium</Text>
      </View>
      <View style={styles.information}>
        <Text style={styles.date}>20/06/2024</Text>
        <View style={styles.buttons}>
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
          source={{uri: ""}}
          style={styles.image}
        />
      </View>
      <View style={styles.details}>
        <View style={styles.location}>
          <Text style={styles.locationText}>
            Đ. 30 Tháng 4, Phú Thọ, Thủ Dầu Một, Bình
            Dương
          </Text>
        </View>
        <View style={styles.pitchStatus}>
          <Text style={styles.pitchStatusText}>Pitch ready:</Text>
        </View>
        <View style={styles.pitchOptions}>
          <TouchableOpacity style={styles.pitchOption}>
            <Text style={styles.pitchOptionText}>
              1 Empty Football - 120k/h
            </Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.pitchOption}>
            <Text style={styles.pitchOptionText}>
              1 Empty Rugby - 150k/h
            </Text>
          </TouchableOpacity>
        </View>
        <View style={styles.amenities}>
          <Text style={styles.amenitiesText}>
            • Miễn Phí gửi xe
          </Text>
          <Text style={styles.amenitiesText}>
            • Miễn phí nước
          </Text>
          <Text style={styles.amenitiesText}>
            • Có phòng thay đồ riêng
          </Text>
        </View>
        <TouchableOpacity style={styles.bookingButton}>
          <Text style={styles.bookingButtonText}>Booking PlayFields</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};
const styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: '#fff',
    },
    header: {
      flexDirection: 'row',
      alignItems: 'center',
      padding: 16,
    },
    backButton: {
      padding: 8,
    },
    backButtonText: {
      fontSize: 20,
      fontWeight: 'bold',
    },
    title: {
      fontSize: 24,
      fontWeight: 'bold',
      marginLeft: 16,
    },
    information: {
      padding: 16,
    },
    date: {
      fontSize: 16,
      marginBottom: 8,
    },
    buttons: {
      flexDirection: 'row',
      justifyContent: 'space-between',
    },
    button: {
      padding: 8,
      borderRadius: 8,
      backgroundColor: '#007bff',
    },
    buttonText: {
      color: '#fff',
      fontSize: 16,
    },
    imageContainer: {
      alignItems: 'center',
    },
    image: {
      width: '100%',
      height: 200,
    },
    details: {
      padding: 16,
    },
    location: {
      marginBottom: 8,
    },
    locationText: {
      fontSize: 16,
    },
    pitchStatus: {
      marginBottom: 8,
    },
    pitchStatusText: {
      fontSize: 16,
      fontWeight: 'bold',
    },
    pitchOptions: {
      marginBottom: 8,
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
    amenities: {
      marginBottom: 8,
    },
    amenitiesText: {
      fontSize: 16,
    },
    bookingButton: {
      padding: 12,
      borderRadius: 8,
      backgroundColor: '#007bff',
    },
    bookingButtonText: {
      color: '#fff',
      fontSize: 18,
      fontWeight: 'bold',
      textAlign: 'center',
    },
  });
export default DetailBookingPage;