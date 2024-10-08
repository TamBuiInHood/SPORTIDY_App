import React, { useState, useEffect } from 'react';
import { View, Text, FlatList, StyleSheet, ActivityIndicator, TouchableOpacity, Image } from 'react-native';
import { NativeBaseProvider, Button } from 'native-base';
import api from '../../config/axios';
import ClubItem from '../../components/ClubItem'; 
import { Club } from '@/types/types'; 

interface Club {
  clubId: number;
  clubCode: string;
  clubName: string;
  regulation: string;
  information: string;
  slogan: string;
  mainSport: string;
  createDate: string;
  location: string;
  totalMember: number;
  avatarClub: string;
  coverImageClub: string;
  listMember: any[];
}




const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#F8F8F8',
    paddingHorizontal: 16, // Padding ngang
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginVertical: 16, 
  },
  headerTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    color: '#333',
    // fontFamily: 'YOUR_FONT_FAMILY', 
  },
  yourClubButton: {
     backgroundColor: '#FFCC00',
     borderRadius: 8,
     paddingVertical: 8,
     paddingHorizontal: 16,


  },
  yourClubButtonText: {
    color: 'white',
    fontWeight: 'bold',


  },
  // ... other styles

  clubItemContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
    backgroundColor: '#fff',
    borderRadius: 12,
    padding: 16,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 4,
    elevation: 3,
  },
  clubImage: {
    width: 80,
    height: 80,
    borderRadius: 40,
    marginRight: 16,
  },
  clubName: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#333',
    // fontFamily: 'YOUR_FONT_FAMILY' 
  },
  clubDetail: {
    fontSize: 14,
    color: '#666',
    // fontFamily: 'YOUR_FONT_FAMILY'
  },
});


const ViewAllClubsHeader = ({ navigation }) => (
 <View style={styles.header}>
    <Text style={styles.headerTitle}>View All Club</Text>
    <TouchableOpacity style={styles.yourClubButton}
    onPress={() => navigation.navigate('YourClub')}
    >

      <Text style={styles.yourClubButtonText}>Your Club</Text>
    </TouchableOpacity>


 </View>
);

const ViewAllClubsScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [clubs, setClubs] = useState<Club[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    const fetchClubs = async () => {
      try {
        const response = await api.get(`/clubs`);
        setClubs(response.data.data.list);
      } catch (error) {
        setError(error);
      } finally {
        setLoading(false);
      }
    };
    fetchClubs();
  }, []);

  if (loading) {
    return <ActivityIndicator size="large" color="#007bff" />;
  }

  if (error) {
    return <Text>Error: {error.message}</Text>;
  }

  if (clubs.length === 0) {
    return (
      <View style={styles.emptyMessageContainer}>
        <Text style={styles.emptyMessage}>No clubs found.</Text>
      </View>
    );
  }

  return (
    <NativeBaseProvider>
       <View style={styles.container}>
         <ViewAllClubsHeader navigation={navigation} />
         <FlatList
           data={clubs}
           keyExtractor={(item) => item.clubId.toString()} // Nên sử dụng key unique hơn nếu có
           renderItem={({ item }) => (
             <ClubItem
               club={item}
               onPress={() => navigation.navigate('ClubDetail', { club: item })}
             />
           )}
         />
       </View>
     </NativeBaseProvider>
  );

}
  export default ViewAllClubsScreen; 