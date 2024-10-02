import React, { useState, useEffect } from 'react';
import { View, Text, Image, StyleSheet, FlatList, TouchableOpacity, ActivityIndicator } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

interface Playfield {
  id: number;
  name: string;
  location: string;
  price: string;
  image: string;
}

export const PlayfieldCard = ({ playfield }: { playfield: Playfield }) => {
  return (
    <View style={styles.card}>
      <Image source={{ uri: playfield.image }} style={styles.image} />
      <View style={styles.infoContainer}>
        <Text style={styles.name}>{playfield.name}</Text>
        <View style={styles.locationRow}>
          <Ionicons name="location-outline" size={16} color="#888" />
          <Text style={styles.location}>{playfield.location}</Text>
        </View>
        <View style={styles.priceRow}>
          <Ionicons name="cash-outline" size={16} color="#888" />
          <Text style={styles.price}>{playfield.price}</Text>
        </View>
        <TouchableOpacity>
          <Text style={styles.viewMore}>View more →</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

// Fake API call function to simulate fetching playfields
const fetchPlayfields = async (): Promise<Playfield[]> => {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve([
        {
          id: 1,
          name: 'My Dinh National Stadium',
          location: 'Nam Từ Liêm, Hà Nội',
          price: '500,000,000 VND',
          image: 'https://yourimageurl.com/stadium1.jpg',
        },
        {
          id: 2,
          name: 'Hang Day Stadium',
          location: 'Đống Đa, Hà Nội',
          price: '300,000,000 VND',
          image: 'https://yourimageurl.com/stadium2.jpg',
        },
        {
          id: 3,
          name: 'Thống Nhất Stadium',
          location: 'Quận 10, TP.HCM',
          price: '200,000,000 VND',
          image: 'https://yourimageurl.com/stadium3.jpg',
        },
        {
          id: 4,
          name: 'Lạch Tray Stadium',
          location: 'Ngô Quyền, Hải Phòng',
          price: '150,000,000 VND',
          image: 'https://yourimageurl.com/stadium4.jpg',
        },
      ]);
    }, 1000);
  });
};

const PlayfieldList = () => {
  const [playfields, setPlayfields] = useState<Playfield[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const loadPlayfields = async () => {
      try {
        const data = await fetchPlayfields();
        setPlayfields(data);
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

  return (
    <FlatList
      data={playfields}
      keyExtractor={(item) => item.id.toString()}
      renderItem={({ item }) => <PlayfieldCard playfield={item} />}
      contentContainerStyle={styles.list}
    />
  );
};

export default PlayfieldList;

const styles = StyleSheet.create({
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
});
