import React, { useState, useEffect } from 'react';
import { View, Text, FlatList, TouchableOpacity } from 'react-native';
import axios from 'axios';
import { NativeBaseProvider, HStack, Button } from 'native-base'; // ếu sử dụng NativeBNase
import ClubItem from '../../components/ClubItem'; // Component hiển thị từng club


const ViewAllClubsScreen = ({ navigation }) => {
  const [clubs, setClubs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchClubs = async () => {
      try {
        const response = await axios.get('/api/clubs');
        setClubs(response.data);
      } catch (err) {
        console.error("Error fetching clubs:", err);
        setError(err);
      } finally {
        setLoading(false);
      }
    };

    fetchClubs();
  }, []);


  if (loading) {
    return <Text>Loading...</Text>; // Hiển thị loading indicator
  }

  if (error) {
    return <Text>Error: {error.message}</Text>; // Hiển thị thông báo lỗi
  }


  return (
    <NativeBaseProvider> {/* Nếu sử dụng NativeBase */}
        <View style={{ flex: 1, padding: 16}}>
        <HStack justifyContent="space-between" alignItems="center" mb={4}> {/* Header */}
                <Text fontSize="lg" fontWeight="bold">View All Clubs</Text>
                <Button onPress={() => navigation.navigate('YourClub')}>Your Club</Button>
            </HStack>
            <FlatList
                data={clubs}
                keyExtractor={(item) => item.id.toString()}
                renderItem={({ item }) => (
                <TouchableOpacity onPress={() => navigation.navigate('ClubDetail', { club: item })}>
                        <ClubItem club={item} />
                 </TouchableOpacity>
                )}
            />
        </View>
    </NativeBaseProvider>
  );
};

export default ViewAllClubsScreen;


