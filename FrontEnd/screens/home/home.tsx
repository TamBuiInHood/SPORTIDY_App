import React, { useEffect, useState } from 'react';
import { View, Text, Image, StyleSheet, TouchableOpacity, Dimensions, ActivityIndicator } from 'react-native';
import Swiper from 'react-native-deck-swiper';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { Ionicons } from '@expo/vector-icons';
import SearchBar from '@/components/SearchBar';
import ActionButtons from '@/components/ActionButton';
import ActionIcons from '@/components/ActionIcons';
import api from '@/config/api';
import { Card } from '@/types/types';
import axios from 'axios';



const HomeScreen: React.FC = () => {
  const [cards, setCards] = useState<Card[]>([]);
  const [likedCards, setLikedCards] = useState<Card[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const navigation = useNavigation<NativeStackNavigationProp<any>>();

  const fetchMeetings = async () => {
    try {
      const response = await axios.get( 'https://65a09e6c600f49256fb01938.mockapi.io/api/tools');
      setCards(response.data); 
    } catch (error) {
      console.error('Failed to fetch meetings:', error);
      setError('Failed to load meetings. Please try again later.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchMeetings();
  }, []);

  const onSwipedLeft = (cardIndex: number) => {
    console.log('Skipped:', cards[cardIndex]);
  };

  const onSwipedRight = (cardIndex: number) => {
    setLikedCards([...likedCards, cards[cardIndex]]);
    console.log('Liked:', cards[cardIndex]);
  };

  const renderCard = (card: Card) => (
    <View style={styles.card}>
      <View style={styles.cardHeader}>
        <Text style={styles.headerTitle}>Meeting</Text>
        <TouchableOpacity style={styles.headerIcon}>
          <Ionicons name="football" size={24} color="black" />
        </TouchableOpacity>
      </View>
      <View style={styles.headerLine} />
      <Image style={styles.image} source={{ uri: card.image }} />
      <View style={styles.cardContent}>
        <View style={styles.infoLeft}>
          <Text style={styles.infoTitle}>{card.artName || 'Unnamed Meeting'}</Text>
          <View style={styles.location}>
            <Ionicons name="location-sharp" size={16} color="black" />
            <Text style={styles.locationText}>{card.artName}</Text>
          </View>
          <Text style={styles.time}>{new Date(card.brand).toLocaleTimeString()}</Text>
          <Text style={styles.date}>{new Date(card.brand).toLocaleDateString()}</Text>
        </View>
        <View style={styles.infoRight}>
          <View style={styles.heartIcon}>
            <Ionicons name="heart" size={20} color="blue" />
          </View>
          <View style={styles.stats}>
            <Text style={styles.statNumber}>{`${card.artName}/${card.price}`}</Text>
            <Text style={styles.statNumber}>+0 others</Text>
          </View>
        </View>
      </View>
    </View>
  );

  if (loading) {
    return <ActivityIndicator size="large" color="#0000ff" style={{ flex: 1, justifyContent: 'center' }} />;
  }

  if (error) {
    return <Text style={{ color: 'red', textAlign: 'center' }}>{error}</Text>;
  }

  return (
    <View style={styles.container}>
      <SearchBar />
      <ActionButtons />
      {cards.length > 0 ? (
        <Swiper
          cards={cards}
          renderCard={renderCard}
          onSwipedLeft={onSwipedLeft}
          onSwipedRight={onSwipedRight}
          onTapCard={() => navigation.navigate('(routes)/detail')}
          backgroundColor={'transparent'}
          stackSize={1}
          cardIndex={0}
          infinite={false}
          verticalSwipe={false}
          horizontalSwipe={true}
          containerStyle={styles.swiperContainer}
          cardStyle={styles.cardStyle}
        />
      ) : (
        <Text>No meetings available</Text>
      )}
      <ActionIcons onSwipedLeft={onSwipedLeft} onSwipedRight={onSwipedRight} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'column',
    backgroundColor: '#fff',
    justifyContent: 'center',
    marginTop: 50
  },
  swiperContainer: {
    flex: 1,
    justifyContent: 'center',
    backgroundColor: 'transparent',
  },
  card: {
    width: Dimensions.get('window').width * 0.9,
    height: Dimensions.get('window').height * 0.6,
    borderRadius: 30,
    borderWidth: 1,
    borderColor: '#ddd',
    alignSelf: 'center',
    backgroundColor: '#fff',
    padding: 10,
    marginTop: 100,
    overflow: 'hidden',
  },
  cardStyle: {
    flex: 1,
  },
  cardHeader: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 10,
    position: 'relative'
  },
  headerTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    color: '#F8931E',
    position: 'absolute',
  },
  headerIcon: {
    padding: 5,
    position: 'absolute',
    right: 10,
  },
  headerLine: {
    height: 5,
    backgroundColor: '#F9BC2C',
    marginHorizontal: 20,
    marginVertical: 10,
  },
  image: {
    width: '90%',
    height: "50%",
    borderRadius: 10,
    alignSelf: 'center',
  },
  cardContent: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    padding: 15,
  },
  infoLeft: {
    flex: 3,
    justifyContent: 'center',
  },
  infoRight: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  infoTitle: {
    fontSize: 16,
    fontWeight: 'bold',
    marginBottom: 10,
    color: "#F0962E"
  },
  location: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 10,
  },
  locationText: {
    fontSize: 14,
    marginLeft: 10,
    color: "#AB9C9C"
  },
  time: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 10,
    color: "#F0962E"
  },
  date: {
    fontSize: 16,
    marginBottom: 5,
    color: "#F0962E"
  },
  heartIcon: {
    backgroundColor: '#F0F0F0',
    borderRadius: 50,
    padding: 8,
    marginBottom: 5,
  },
  stats: {
    alignItems: 'center',
  },
  statNumber: {
    fontSize: 14,
    textAlign: 'center',
  },


});

export default HomeScreen;