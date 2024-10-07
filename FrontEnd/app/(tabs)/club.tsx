import React from 'react';
import { View, Text, ScrollView, StyleSheet, TouchableOpacity, Image } from 'react-native';
import SearchBar from '@/components/SearchBar';
import ActionButtonClub from '@/components/Actionbuttonclub';

const ClubScreen: React.FC = () => {
  return (
    <View style={styles.container}>
      <View style={styles.headerContainer}> 
        <SearchBar />
        <ActionButtonClub />
      </View>
      <ScrollView style={styles.content}>
        <View style={styles.clubInfo}>
          <Text style={styles.clubName}>SPORTIDY Night Club</Text>
          <View style={styles.sloganContainer}>
            <Text style={styles.clubSlogan}>CHƠI CÀNG HAY, NHẬU CÀNG SAY</Text>
          </View>
          <Text style={styles.clubTime}>From: 14/10/2024</Text>
          <Image
            source={{
              uri: 'https://cdn.builder.io/api/v1/image/assets/TEMP/cac9029126bb52849507b53ea4ecb5f6ca3e31522c984956a2339b1c1931935b?placeholderIfAbsent=true&apiKey=f8ccf21cf8ce4053b555f169e17dcf6a',
            }}
            style={styles.clubImage}
          />
          <TouchableOpacity style={styles.joinButton}>
            <Text style={styles.joinButtonText}>Join Club</Text>
          </TouchableOpacity>
        </View>
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f9f9f9',
  },
  headerContainer: { 
    position: 'absolute',
    top: 0, 
    left: 0,
    right: 0,
    zIndex: 1,
    padding: 10,
  },
  content: {
    flex: 1,
    paddingTop: 100, 
  },
  clubInfo: {
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#fff9c4',
    padding: 20,
    borderRadius: 10,
    marginBottom: 20,
    marginTop: 60,
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    elevation: 5,
  },
  clubName: {
    fontSize: 20,
    fontWeight: 'bold',
    marginBottom: 10,
    color: '#000000', 
  },
  sloganContainer: {
    backgroundColor: '#FFFFFF',
    borderRadius: 20,
    padding: 5,
    marginBottom: 10, 
  },
  clubSlogan: {
    fontSize: 14,
    color: '#808080',
    textAlign: 'center',
    textTransform: 'uppercase',
  },
  clubTime: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#4B3621',
    marginBottom: 20,
  },
  clubImage: {
    width: '100%',
    height: 200,
    resizeMode: 'contain',
    marginBottom: 20,
    borderRadius: 10,
  },
  joinButton: {
    backgroundColor: '#FFA500',
    padding: 10,
    borderRadius: 30,
    marginTop: 10, 
    paddingHorizontal: 60,
  },
  joinButtonText: {
    color: '#FFFFFF',
    fontWeight: 'bold',
  },

});

export default ClubScreen;