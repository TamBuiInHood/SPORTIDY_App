import React, { useState, useEffect } from 'react';
import { View, Text, Image, StyleSheet, TouchableOpacity, ActivityIndicator, ScrollView } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import SearchBar from '@/components/SearchBar';
import { LinearGradient } from 'expo-linear-gradient';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RootStackParamList } from '@/types/types';
import { useNavigation } from '@react-navigation/native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import axios from 'axios';

interface Playfield {
  playFieldId: number;
  name: string;
  location: string;
  price: string;
  image: string;
  openingHours: string;
  closeHours: string;
  rating: number;
  reviews: number;
  owner: string;
}

type HomeScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "HomeScreen">;

export const PlayfieldCard = ({ playfield }: { playfield: Playfield }) => {
  const navigation = useNavigation<HomeScreenNavigationProp>();
  const handlePress = () => {
    console.log('Navigating to PlayFieldDetail with ID:', playfield.playFieldId);
    navigation.navigate("PlayfieldDetailCard", { playfieldId: playfield.playFieldId });
  };

  return (
    <TouchableOpacity onPress={handlePress}>
    <View style={styles.card}>
      <Image source={{ uri: playfield.avatarImage }} style={styles.image} />
      <View style={styles.infoContainer}>
        <Text style={styles.name}>{playfield.playFieldName}</Text>
        <View style={styles.locationRow}>
          <Ionicons name="location-outline" size={16} color="#888" />
          <Text style={styles.location}>{playfield.address}</Text>
        </View>
        <View style={styles.priceRow}>
          <Ionicons name="cash-outline" size={16} color="#888" />
          <Text style={styles.price}>{playfield.price}</Text>
        </View>
        <View style={styles.priceRow}>
          <Ionicons name="time-outline" size={16} color="#888" />
          <Text style={styles.price}>{playfield.openTime} - {playfield.closeTime}</Text>
        </View>
        <TouchableOpacity onPress={handlePress}>
          <Text style={styles.viewMore}>View more →</Text>
        </TouchableOpacity>
      </View>
    </View>
    </TouchableOpacity>
  );
};

export const fetchPlayfields = async (): Promise<Playfield[]> => {
  try {
    const userId = await AsyncStorage.getItem('userId');

    if (!userId) {
      throw new Error("User ID not found.");
    }

    const response = await fetch('https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/Playfields/get-by-user-id/21');
    const data = await response.json(); // Parsing the response to JSON
    console.log('Fetched data:', data); // Log the fetched data for debugging
    return data; // Return the single playfield object // Adjust this based on the response structure
  } catch (error) {
    console.error("Failed to fetch playfields:", error);
    throw new Error("Failed to load playfields. Please try again later.");
  }
};

const PlayfieldList = () => {
  const [playfields, setPlayfields] = useState<Playfield[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const loadPlayfields = async () => {
      try {
        const response = await fetchPlayfields();
        if (response.isSuccess) {
          setPlayfields(response.data); // Set playfields from response data
        } else {
          setError('Failed to load playfields.');
        }
      } catch (err) {
        setError('Failed to load playfields. Please try again later.');
      } finally {
        setLoading(false);
      }
    };

    loadPlayfields();
  }, []);

  if (loading) {
    return (
      <View style={styles.loadingContainer}>
        <ActivityIndicator size="large" color="#48C9B0" />
      </View>
    );
  }

  if (error) {
    return (
      <View style={styles.errorContainer}>
        <Text style={styles.errorText}>{error}</Text>
      </View>
    );
  }

  if (!playfields.length) {
    return (
      <View style={styles.errorContainer}>
        <Text style={styles.errorText}>No playfields found.</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <SearchBar />
      <View style={styles.header}>
        <LinearGradient colors={["#76B852", "#A0B853"]} style={styles.gradient}>
          <Text style={styles.headerTitle}>View Your Playfields</Text>
        </LinearGradient>
      </View>
      <ScrollView contentContainerStyle={styles.list}>
        {playfields.map((playfield) => (
          <PlayfieldCard key={playfield.playFieldId} playfield={playfield} />
        ))}
      </ScrollView>
    </View>
  );
};

export default PlayfieldList;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f0f0f0',
    paddingTop: 50,
  },
  header: {
    padding: 15,
    marginTop: 50,
    paddingHorizontal: 50,
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: 'bold',
    color: '#fff',
  },
  card: {
    backgroundColor: '#fff',
    borderRadius: 10,
    overflow: 'hidden',
    marginBottom: 15,
    elevation: 2,
    flexDirection: 'row',
    padding: 10,
  },
  image: {
    width: 100,
    height: 100,
    borderRadius: 8,
  },
  infoContainer: {
    flex: 1,
    marginLeft: 10,
  },
  name: {
    fontSize: 16,
    fontWeight: 'bold',
    marginBottom: 5,
  },
  locationRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 5,
  },
  location: {
    marginLeft: 5,
    color: '#888',
  },
  priceRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 10,
  },
  price: {
    marginLeft: 5,
    color: '#888',
  },
  viewMore: {
    color: '#48C9B0',
    fontWeight: 'bold',
  },
  list: {
    padding: 15,
  },
  loadingContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  errorContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  errorText: {
    color: 'red',
    fontSize: 18,
  },
  gradient: {
    width: '100%',
    paddingVertical: 10,
    alignItems: 'center',
    borderRadius: 30,
  },
});
