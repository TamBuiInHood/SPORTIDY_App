import React from 'react';
import { View, Text, Image, StyleSheet } from 'react-native';


const ClubItem = ({ club }: {club: any}) => { //  Đảm bảo đúng kiểu dữ liệu cho club
    return (
        <View style={styles.container}>
            <Image source={{ uri: club.image }} style={styles.image} />
            <View style = {styles.clubInfo}>
              <Text style={styles.name}>{club.name}</Text>
              <Text >Members: {club.members}</Text> {/* Hiển thị số lượng thành viên */}
              <Text>Main Sport: {club.mainSport}</Text> {/* Hiển thị môn thể thao chính */}
              <Text >Location: {club.location}</Text> {/* Hiển thị địa điểm */}
              {/* Hiển thị các thông tin khác của club */}
             </View>
        </View>
    );
};

const styles = StyleSheet.create({
    container:{
        flexDirection: 'row',
        alignItems: 'center',
        padding: 10,
        borderBottomWidth: 1,
        borderBottomColor: '#eee',

    },
    image: {
        width: 50,
        height: 50,
        marginRight: 10,
        borderRadius: 25,
    },
    clubInfo: {
       flex: 1,
    },

    name:{
        fontSize: 16,
        fontWeight: 'bold',

    },
})
export default ClubItem;