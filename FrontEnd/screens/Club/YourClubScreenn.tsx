import React, { useState, useEffect } from 'react';
import { View, Text, FlatList, StyleSheet, ActivityIndicator } from 'react-native';
import api from '../../config/axios';
import ClubItem from '../../components/ClubItem';


const YourClubScreen = ({ navigation }) => {
  const [yourClubs, setYourClubs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);


  useEffect(() => {
    const fetchYourClubs = async () => {
      try {
        const response = await api.get('/me/clubs'); // Thay thế bằng endpoint của bạn
        setYourClubs(response.data); // Điều chỉnh theo cấu trúc dữ liệu từ API
      } catch (err) {
        setError(err);
        console.error("Error fetching your clubs:", err); // Log lỗi ra console để dễ debug
      } finally {
        setLoading(false);
      }
    };


    fetchYourClubs();
  }, []);


  if (loading) {
    return <ActivityIndicator size="large" style={styles.loadingIndicator} />;
  }


  if (error) {
    return (
      <View style={styles.errorContainer}>
        <Text style={styles.errorText}>Error: {error.message}</Text>
      </View>
    );
  }


  return (
    <View style={styles.container}>
      <Text style={styles.title}>Your Clubs</Text>
      {yourClubs.length > 0 ? (
        <FlatList
          data={yourClubs}
          keyExtractor={(item) => item.clubId.toString()} // Thay bằng key unique hơn nếu có (ví dụ: clubCode)
          renderItem={({ item }) => (
            <ClubItem
              club={item}
              onPress={() => navigation.navigate('ClubDetail', { club: item })}
            />
          )}
          style={styles.flatList}
        />
      ) : (
         <View style={styles.noClubsContainer}>
           <Text style={styles.noClubsText}>You haven't joined any clubs yet.</Text>
         </View>
      )}
    </View>
  );
};




const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
    backgroundColor: '#f8f8f8',
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 16,
    color: '#333',
  },
  loadingIndicator: {
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
  },
  noClubsContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',


  },
  noClubsText: {
    fontSize: 16,
    color: '#888',


  },
  flatList: {
    flex: 1,


  },
});




export default YourClubScreen;