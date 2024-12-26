import { Ionicons } from '@expo/vector-icons';
import { RouteProp, useRoute } from '@react-navigation/native';
import React, { useEffect, useState } from 'react';
import { View, Text, Image, TouchableOpacity, StyleSheet, FlatList } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { PlayField, RootStackParamList } from '@/types/types';
import api from '@/config/api';

type DetailBookingRouteProp = RouteProp<RootStackParamList, 'DetailBookingPage'>;

const DetailBookingPage: React.FC = () => {
  const route = useRoute<DetailBookingRouteProp>();
  const navigation = useNavigation();
  const formatTime = (time: any) => {
    if (!time) return '--';
    const [hours, minutes] = time.split(':');
    return `${hours.padStart(2, '0')}:${minutes.padStart(2, '0')}`;
  };
  const playId = route.params?.playFieldId;

  const [playfields, setPlayFields] = useState<PlayField | null>(null);
  const [loading, setLoading] = useState<boolean>(true); // Loading state to manage API call state
  const handlePress = () => {
    if (playfields) {
      navigation.navigate('BookingInformationPage', {
        playFieldId: playfields.playFieldId,
        playFieldName: playfields.playFieldName,
        price: 120000,
        address: playfields.address,
      });
    } else {
      console.error('Playfields data is not available');
    }
  };
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
  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.backButton}>
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

        <Text style={styles.pitchStatusText}>Pitch ready:</Text>
        {/* <FlatList
          data={playfields?.price}
          renderItem={({ item }) => (
            <View style={styles.pitchOption}>
              <Text style={styles.pitchOptionText}>{item.name} - {item.price}</Text>
            </View>
          )}
          keyExtractor={item => item.name}
        />

        <Text style={styles.servicesText}>Services:</Text>
        {playfield.services.map((service, index) => (
          <Text key={index} style={styles.amenitiesText}>• {service}</Text>
        ))} */}

        <TouchableOpacity style={styles.bookingButton} onPress={handlePress}>
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
    paddingTop: 30,
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
    marginTop: 10,
    marginHorizontal: 60
  },
  bookingButtonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
    textAlign: 'center',
  },
});

export default DetailBookingPage;
