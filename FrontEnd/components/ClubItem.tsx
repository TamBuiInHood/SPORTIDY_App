import React from 'react';
import { View, Text, StyleSheet, Image, TouchableOpacity } from 'react-native';

interface Club {
  clubId: number;
  clubName: string;
  avatarClub: string;
  totalMember: number;
  mainSport: string;
  location: string;
  // ...thêm các thuộc tính khác nếu cần
}

const ClubItem: React.FC<{ club: Club; onPress: () => void }> = ({ club, onPress }) => {
    return (
      <TouchableOpacity onPress={onPress} style={styles.container}>
        <Image
          source={{ uri: club.avatarClub }}
          style={styles.image}
          onError={(error) => console.error('Error loading image:', error)} // Xử lý lỗi ảnh
          defaultSource={require('../assets/images/placehoder.png')} // Hiển thị placeholder
        />
        <View style={styles.info}>
          <Text style={styles.name}>{club.clubName}</Text>
          {/* ... other club details */}
        </View>
      </TouchableOpacity>
    );
  };
  
  const styles = StyleSheet.create({
    container: {
      flexDirection: 'row',
      alignItems: 'center',
      padding: 16,
      backgroundColor: 'white',
      marginBottom: 8,
      borderRadius: 8,
      // ... other styles
    },
    image: {
      width: 60,
      height: 60,
      borderRadius: 30,
      marginRight: 12,
    },
    info: {
        justifyContent: 'center'
  
  
    },
    name: {
      fontSize: 16,
      fontWeight: 'bold',
    },
  
  infoText: {
    fontSize: 14,
    color: '#666',
  },
});


export default ClubItem;