import React from 'react';
import { View, Text, Image, StyleSheet, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

interface PlayfieldDetailProps {
  playfield: {
    name: string;
    location: string;
    openingHours: string;
    capacity: string;
    surface: string;
    owner: string;
    rating: number;
    reviews: number;
    image: string;
    price: string;
  };
}

const PlayfieldDetail = ({ playfield }: PlayfieldDetailProps) => {
  return (
    <View style={styles.container}>
      <Image source={{ uri: playfield.image }} style={styles.image} />
      <View style={styles.infoContainer}>
        <Text style={styles.name}>{playfield.name}</Text>
        <Text style={styles.location}>{playfield.location}</Text>
        <View style={styles.ratingContainer}>
          <Text style={styles.rating}>{playfield.rating} ★</Text>
          <Text style={styles.reviews}>({playfield.reviews} reviews)</Text>
        </View>
        <Text style={styles.detailsTitle}>Details:</Text>
        <Text style={styles.details}>Open: {playfield.openingHours}</Text>
        <Text style={styles.details}>Capacity: {playfield.capacity}</Text>
        <Text style={styles.details}>Surface: {playfield.surface}</Text>
        <Text style={styles.details}>Owner: {playfield.owner}</Text>
        <View style={styles.priceContainer}>
          <Text style={styles.priceLabel}>Price:</Text>
          <Text style={styles.price}>{playfield.price}</Text>
        </View>
      </View>
      <TouchableOpacity style={styles.updateButton}>
        <Text style={styles.updateButtonText}>Update</Text>
      </TouchableOpacity>
    </View>
  );
};

// Sample data for the playfield detail
const samplePlayfield = {
  name: 'My Dinh National Stadium',
  location: 'Nam Từ Liêm, Hà Nội',
  openingHours: '5:00 - 22:00',
  capacity: '40,000 people',
  surface: 'Grass',
  owner: 'Vietnamese government',
  rating: 4.0,
  reviews: 480,
  image: 'https://yourimageurl.com/stadium.jpg', // Replace with actual image URL
  price: '500,000,000 VND',
};

const PlayFieldDetailCard = () => {
  return (
    <View style={{ flex: 1 }}>
      <PlayfieldDetail playfield={samplePlayfield} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f9f9f9',
    borderRadius: 10,
    margin: 16,
    elevation: 2,
  },
  image: {
    width: '100%',
    height: 200,
    borderTopLeftRadius: 10,
    borderTopRightRadius: 10,
  },
  infoContainer: {
    padding: 16,
  },
  name: {
    fontSize: 20,
    fontWeight: 'bold',
    marginBottom: 4,
  },
  location: {
    fontSize: 16,
    color: '#888',
    marginBottom: 8,
  },
  ratingContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 12,
  },
  rating: {
    fontSize: 16,
    fontWeight: 'bold',
    marginRight: 4,
  },
  reviews: {
    fontSize: 14,
    color: '#888',
  },
  detailsTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    marginVertical: 8,
  },
  details: {
    fontSize: 16,
    marginBottom: 4,
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
