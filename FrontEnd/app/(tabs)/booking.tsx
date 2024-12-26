import React, { useEffect, useState } from 'react';
import { View, Text, ScrollView, StyleSheet, Image, TouchableOpacity, Button } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { FlatList } from 'react-native-gesture-handler';
import SearchBar from '@/components/SearchBar';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import Sidebar from '@/components/Sidebar';
import { PlayField, RootStackParamList } from '@/types/types';
import api from '@/config/api';

type BookingScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'BookingScreen'>;

const BookingScreen: React.FC = () => {
  const navigation = useNavigation<BookingScreenNavigationProp>();

  const [playfields, setPlayFields] = useState<PlayField[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [isSidebarVisible, setSidebarVisible] = useState<boolean>(false);
  const toggleSidebar = () => {
    setSidebarVisible(prevState => !prevState);
  };
  const fetchData = async () => {
    try {
      const response = await api.getAllPlayField(0, 20);
      console.log('response:', response);
      console.log('response.data:', response.data);
      const result: PlayField = response.data;
      console.log('result:', result);

      if (result && result.list && Array.isArray(result.list)) {
        setPlayFields(result.list);
      } else {
        throw new Error('Unexpected data format: data.list is not an array');
      }
    } catch (error) {
      console.error('Failed to fetch meetings:', error);
      setError('Failed to load meetings. Please try again later.');
    } finally {
      setLoading(false);
    }
  };
  useEffect(() => {
    fetchData();
  }, []);

  const recentPlayfields = playfields.slice(0, 3);
  const bestPlayfields = playfields
    .sort((a, b) => a.price - b.price)
    .slice(0, 3);
  const nearestPlayfields = playfields.filter(playfield => playfield.address.includes("Hanoi"));
  const renderPlayfields = (playfields: PlayField[]) => (
    <FlatList
      data={playfields}
      renderItem={({ item }) => (
        <TouchableOpacity
          key={item.playFieldId}
          style={styles.playfieldCard}
          onPress={() => navigation.navigate('DetailBookingPage', { playFieldId: item.playFieldId })}
        >
          <Image source={{ uri: item.avatarImage }} style={styles.playfieldImage} />
          <Text style={styles.playfieldName}>{item.playFieldName}</Text>
          <Text style={styles.playfieldLocation}>{item.address}</Text>
        </TouchableOpacity>
      )}
      keyExtractor={item => item.playFieldId.toString()}
      horizontal
      showsHorizontalScrollIndicator={false}
      contentContainerStyle={styles.playfieldContainer}
    />
  );

  return (
    <ScrollView style={styles.container}>
      <View style={{ flex: 1, marginLeft: 10 }}>
        <TouchableOpacity onPress={toggleSidebar} style={styles.iconButton}>
          <Text>
            <Ionicons name="menu-outline" size={24} color="#000" />
          </Text>
        </TouchableOpacity>
        <Sidebar isVisible={isSidebarVisible} onClose={toggleSidebar} />
      </View>
      <View style={styles.searchContainer}>
        <SearchBar />
      </View>

      <TouchableOpacity style={styles.bookingButton}>
        <Text style={styles.bookingButtonText}>Booking PlayFields</Text>
      </TouchableOpacity>

      <Text style={styles.sectionTitle}>Recent Playfields</Text>
      {renderPlayfields(recentPlayfields)}

      <Text style={styles.sectionTitle}>Best Playfields</Text>
      {renderPlayfields(bestPlayfields)}

      <Text style={styles.sectionTitle}>Nearest Playfields</Text>
      {renderPlayfields(nearestPlayfields)}
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingTop: 30,
    backgroundColor: '#fff',
    marginBottom: 50
  },
  searchContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 80,
  },
  locationText: {
    marginLeft: 10,
    fontSize: 16,
    fontWeight: 'bold',
  },
  bookingButton: {
    backgroundColor: '#f39c12',
    borderRadius: 50,
    paddingVertical: 15,
    alignItems: 'center',
    marginVertical: 20,
    marginHorizontal: 80
  },
  bookingButtonText: {
    fontSize: 18,
    color: '#fff',
  },
  sectionTitle: {
    fontSize: 20,
    fontWeight: 'bold',
    marginBottom: 10,
    marginLeft: 20
  },
  playfieldContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingBottom: 10,
    paddingHorizontal: 5,
  },
  playfieldCard: {
    width: 180,
    borderRadius: 10,
    backgroundColor: '#f9f9f9',
    padding: 10,
    alignItems: 'center',
    elevation: 2,
    marginHorizontal: 5,
  },
  playfieldImage: {
    width: '100%',
    height: 100,
    borderRadius: 10,
    marginBottom: 10,
  },
  playfieldName: {
    fontSize: 14,
    fontWeight: 'bold',
    marginBottom: 5,
  },
  playfieldLocation: {
    fontSize: 12,
    color: '#888',
  },
});

export default BookingScreen;
