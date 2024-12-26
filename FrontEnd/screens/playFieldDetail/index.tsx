import React from 'react';
import { View, Text, Image, StyleSheet, TouchableOpacity, ScrollView } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { LinearGradient } from 'expo-linear-gradient';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RootStackParamList } from '@/types/types';
import { useNavigation } from '@react-navigation/native';

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
type HomeScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "HomeScreen">;

const PlayfieldDetail = ({ playfield }: PlayfieldDetailProps) => {
  const navigation = useNavigation<HomeScreenNavigationProp>();

  const renderStars = (rating: number) => {
    const stars = [];
    for (let i = 1; i <= 5; i++) {
      stars.push(
        <Ionicons
          key={i}
          name={i <= rating ? 'star' : 'star-outline'}
          size={20}
          color={i <= rating ? '#FFD700' : '#ccc'} // Màu vàng cho rating, xám cho không rating
        />
      );
    }
    return stars;
  };

  return (
    <View style={styles.container}>
      <Image source={{ uri: playfield.image }} style={styles.image} />
      <View style={styles.infoContainer}>
        <View style={styles.locationContainer}>
          <Ionicons name='location-outline' size={30} />
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
      <TouchableOpacity style={styles.updateButton}onPress={()=>navigation.navigate("UpdatePlayfield")}>
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
  image: 'https://babolat.com.vn/wp-content/uploads/2023/12/san-cau-long-chat-luong-4.jpg', // Replace with actual image URL
  price: '500,000,000 VND',
};

const PlayFieldDetailCard = () => {
  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <LinearGradient colors={["#76B852", "#A0B853"]} style={styles.gradient}>
          <Text style={styles.headerTitle}>{samplePlayfield.name}</Text>
        </LinearGradient>
      </View>
      <ScrollView>
        <PlayfieldDetail playfield={samplePlayfield} />
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f9f9f9',
    borderRadius: 10,
    elevation: 2,
  },
  image: {
    width: '100%',
    height: 200,

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
    fontSize: 20,
    color: 'black',
    marginBottom: 8,
    marginLeft: 8,
    fontWeight: 'bold'
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
  header: {
    marginTop: 40,
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: 'bold',
    color: '#fff',
    paddingVertical: 55
  },
  gradient: {
    width: '100%',
    alignItems: 'center',
  },
  locationContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },

});

export default PlayFieldDetailCard;
