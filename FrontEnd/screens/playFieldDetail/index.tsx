import React from 'react';
import { View, Text, Image, StyleSheet, TouchableOpacity, ScrollView } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { LinearGradient } from 'expo-linear-gradient';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RootStackParamList } from '@/types/types';
import { RouteProp, useNavigation } from '@react-navigation/native';

type PlayfieldDetailScreenRouteProp = RouteProp<RootStackParamList, 'PlayFieldDetail'>;
type PlayfieldDetailScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'PlayFieldDetail'>;

interface PlayfieldDetailScreenProps {
  route: PlayfieldDetailScreenRouteProp;
}

const PlayFieldDetailCard = ({ route }: PlayfieldDetailScreenProps) => {
  const navigation = useNavigation<PlayfieldDetailScreenNavigationProp>();
  const { playfield } = route.params; // Lấy dữ liệu playfield từ tham số 'route'

  const renderStars = (rating: number) => {
    const stars = [];
    for (let i = 1; i <= 5; i++) {
      stars.push(
        <Ionicons
          key={i}
          name={i <= rating ? 'star' : 'star-outline'}
          size={20}
          color={i <= rating ? '#FFD700' : '#ccc'}
        />
      );
    }
    return stars;
  };

  return (
    <View style={styles.container}>
       <LinearGradient colors={['#76B852', '#A0B853']} style={styles.header}>
        <Text style={styles.headerTitle}>{playfield.name}</Text>
      </LinearGradient>
      <Image source={{ uri: playfield.image }} style={styles.image} />
      <View style={styles.infoContainer}>
        <View style={styles.locationContainer}>
          <Ionicons name="location-outline" size={30} />
          <Text style={styles.location}>{playfield.location}</Text>
        </View>
        <View style={styles.ratingContainer}>
          <View style={styles.stars}>
            {renderStars(playfield.rating)}
          </View>
          <Text style={styles.reviews}>({playfield.reviews} reviews)</Text>
        </View>
        <Text style={styles.details}>Open: {playfield.openingHours}</Text>
        <Text style={styles.details}>Capacity: {playfield.capacity}</Text>
        <Text style={styles.details}>Surface: {playfield.surface}</Text>
        <Text style={styles.details}>Owner: {playfield.owner}</Text>
        <View style={styles.priceContainer}>
          <Text style={styles.priceLabel}>Price:</Text>
          <Text style={styles.price}>{playfield.price}</Text>
        </View>
      </View>
      <TouchableOpacity
        style={styles.updateButton}
        onPress={() => navigation.navigate('UpdatePlayfield', {playfield})}
      >
        <Text style={styles.updateButtonText}>Update</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f9f9f9',
    paddingTop: 40,
  },
  header: {
    paddingVertical: 40,
    alignItems: 'center',
    justifyContent: 'center',
  
  },
  headerTitle: {
    fontSize: 24,
    color: '#fff',
    fontWeight: 'bold',
  },
  image: {
    width: '100%',
    height: 200,
  },
  infoContainer: {
    padding: 16,
  },
  location: {
    fontSize: 20,
    color: 'black',
    marginBottom: 8,
    fontWeight: 'bold',
  },
  stars: {
    flexDirection: 'row',
  },
  ratingContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    marginBottom: 12,
  },
  reviews: {
    fontSize: 14,
    color: '#888',
  },
  details: {
    fontSize: 16,
    marginBottom: 10,
  },
  priceContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginVertical: 8,
  },
  priceLabel: {
    fontSize: 16,
    fontWeight: 'bold',
    marginRight: 4,
  },
  price: {
    fontSize: 16,
    color: '#48C9B0',
  },
  updateButton: {
    backgroundColor: '#48C9B0',
    borderRadius: 8,
    paddingVertical: 10,
    paddingHorizontal: 20,
    alignItems: 'center',
    margin: 16,
  },
  updateButtonText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },
});

export default PlayFieldDetailCard;
