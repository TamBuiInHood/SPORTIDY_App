import React, { useState, useEffect } from 'react';
import { View, Text, Image, StyleSheet, FlatList, TouchableOpacity, ActivityIndicator, ScrollView } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import SearchBar from '@/components/SearchBar';
import { LinearGradient } from 'expo-linear-gradient';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RootStackParamList } from '@/types/types';
import { useNavigation } from '@react-navigation/native';

interface Playfield {
  id: number;
  name: string;
  location: string;
  price: string;
  image: string;
  openingHours: string;
  rating: number;
  reviews: number;
  owner: string;
}
type HomeScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "HomeScreen">;

export const PlayfieldCard = ({ playfield }: { playfield: Playfield }) => {
  const navigation = useNavigation<HomeScreenNavigationProp>();

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
        <TouchableOpacity onPress={() => navigation.navigate("PlayfieldDetailCard", { playfield })}>
          <Text style={styles.viewMore}>View more →</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

export const fetchPlayfields = async (): Promise<Playfield[]> => {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve([
        {
          id: 1,
          name: 'Cầu Lông Bảo Thy',
          location: '32/17 Ðuờng DT741, Ấp Vinh Tiến, Xã Vinh Hoà , Phú Giáo',
          price: '200,000 VND',
          image: 'https://i.pinimg.com/564x/6c/7a/e8/6c7ae8588a36b4b392c0824f17fcf2cc.jpg',
          openingHours: '7:00 - 21:00',
          rating: 4.0,
          reviews: 480,
          owner: "Trần Thị B"
        },
        {
          id: 2,
          name: 'Sân Banh Thủ Ðức',
          location: '35/11 Ð. Số 4, Truờng Thạnh, Thủ Ðức, Hồ Chí Minh, Vietnam',
          price: '15,000 VND',
          image: 'https://i.pinimg.com/564x/4f/42/97/4f42976671d69d284f63fd8ace21576b.jpg',
          openingHours: '9:00 - 22:00',
          rating: 4.5,
          reviews: 350,
          owner: "Nguyễn Văn A"
        },
        {
          id: 3,
          name: 'Cầu Lông Bảo Thy - Sub 2',
          location: '32/17 Ðuờng DT741, Ấp Vinh Tiến, Xã Vinh Hoà , Phú Giáo',
          price: '100,000 VND',
          image: 'https://i.pinimg.com/564x/ee/94/c1/ee94c15a09ea8ade6ca1f46cdfb65412.jpg',
          openingHours: '7:00 - 21:00',
          rating: 4.8,
          reviews: 400,
          owner: "Trần Thị B"
        },
        {
          id: 4,
          name: 'Sân Banh Thủ Ðức',
          location: '35/11 Ð. Số 4, Truờng Thạnh, Thủ Ðức, Hồ Chí Minh, Vietnam',
          price: '300,000 VND',
          image: 'https://i.pinimg.com/564x/0e/90/27/0e9027444818e9f846d46de954f55b7e.jpg',
          openingHours: '6:00 - 22:00',
          owner: 'Nguyễn Văn A',
          rating: 4.5,
          reviews: 350,
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
    <View style={styles.container}>
      <SearchBar />
      <View style={styles.header}>
        <LinearGradient colors={["#76B852", "#A0B853"]} style={styles.gradient}>
          <Text style={styles.headerTitle}>View Your Playfields</Text>
        </LinearGradient>
      </View>
      <ScrollView contentContainerStyle={styles.list}>
        {playfields.map((playfield) => (
          <PlayfieldCard key={playfield.id} playfield={playfield} />
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
    paddingTop: 50
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
    borderRadius: 30
  },
});
