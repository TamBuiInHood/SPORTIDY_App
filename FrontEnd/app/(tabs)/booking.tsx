import React from 'react';
import { View, Text, ScrollView, StyleSheet, Image, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { FlatList } from 'react-native-gesture-handler';
import SearchBar from '@/components/SearchBar';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';

interface Playfield {
  id: number;
  name: string;
  location: string;
  image: string;
}

const BookingScreen: React.FC = () => {
  const navigation = useNavigation<NativeStackNavigationProp<any>>();

  const recentPlayfields: Playfield[] = [
    { id: 1, name: 'Go Dau Stadium', location: 'Thu Dau Mot, Binh Duong', image: 'https://i.pinimg.com/564x/cc/ad/53/ccad53997147640eed2da368eea00783.jpg' },
    { id: 2, name: 'Pickleball Vuon Lan', location: 'District 7, Ho Chi Minh City', image: 'https://i.pinimg.com/564x/40/98/2a/40982a8167f0a53dedce3731178f2ef5.jpg' },
    { id: 3, name: 'Pickleball Vuon Lan', location: 'District 7, Ho Chi Minh City', image: 'https://i.pinimg.com/564x/40/98/2a/40982a8167f0a53dedce3731178f2ef5.jpg' },

  ];

  const bestPlayfields: Playfield[] = [
    { id: 1, name: 'Santiago Bernabeu', location: 'Av. de Concha Espina, Spain', image: 'https://i.pinimg.com/564x/05/e4/fe/05e4fe1aa8d79e7892539ff0214d7015.jpg' },
    { id: 2, name: 'New Sai Gon Fields', location: 'District 7, Ho Chi Minh City', image: 'https://i.pinimg.com/564x/05/e4/fe/05e4fe1aa8d79e7892539ff0214d7015.jpg' },
    { id: 3, name: 'Pickleball Vuon Lan', location: 'District 7, Ho Chi Minh City', image: 'https://i.pinimg.com/564x/40/98/2a/40982a8167f0a53dedce3731178f2ef5.jpg' },

  ];

  const nearestPlayfields: Playfield[] = [
    { id: 1, name: 'Thong Nhat Stadium', location: 'District 1, Thu Duc City', image: 'https://i.pinimg.com/564x/cc/ad/53/ccad53997147640eed2da368eea00783.jpg' },
    { id: 2, name: 'Pickleball Vuon Lan', location: 'District 7, Ho Chi Minh City', image: 'https://i.pinimg.com/564x/40/98/2a/40982a8167f0a53dedce3731178f2ef5.jpg' },
    { id: 3, name: 'Pickleball Vuon Lan', location: 'District 7, Ho Chi Minh City', image: 'https://i.pinimg.com/564x/40/98/2a/40982a8167f0a53dedce3731178f2ef5.jpg' },

  ];

  const renderPlayfields = (playfields: Playfield[]) => (
    <FlatList
      data={playfields}
      renderItem={({ item }) => (
        <TouchableOpacity
          key={item.id}
          style={styles.playfieldCard}
          onPress={() => navigation.navigate('BookingDetail', { playfield: item })}
        >
          <Image source={{ uri: item.image }} style={styles.playfieldImage} />
          <Text style={styles.playfieldName}>{item.name}</Text>
          <Text style={styles.playfieldLocation}>{item.location}</Text>
        </TouchableOpacity>
      )}
      keyExtractor={item => item.id.toString()}
      horizontal
      showsHorizontalScrollIndicator={false}
      contentContainerStyle={styles.playfieldContainer}
    />
  );

  return (
    <ScrollView style={styles.container}>
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
    marginBottom: 20,
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
