import React, { useState } from 'react';
import { View, Text, Image, StyleSheet, TouchableOpacity, Dimensions } from 'react-native';
import Swiper from 'react-native-deck-swiper';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { Ionicons } from '@expo/vector-icons';
import SearchBar from '@/components/SearchBar';
import ActionButtons from '@/components/ActionButton';
import ActionIcons from '@/components/ActionIcons';

interface Card {
  id: number;
  image: string;
  title: string;
  location: string;
  time: string;
  date: string;
}

const cards: Card[] = [
  { id: 1, image: 'https://i.pinimg.com/564x/cc/ad/53/ccad53997147640eed2da368eea00783.jpg', title: 'Badminton training with Coach TanHuynh', location: 'Phu Giao, Binh Duong', time: '3:30 PM', date: 'Sat 23/6/2024' },
  { id: 2, image: 'https://i.pinimg.com/564x/05/e4/fe/05e4fe1aa8d79e7892539ff0214d7015.jpg', title: 'Another training', location: 'Location', time: 'Time', date: 'Date' },
];

const HomeScreen: React.FC = () => {
  const [likedCards, setLikedCards] = useState<Card[]>([]);
  const navigation = useNavigation<NativeStackNavigationProp<any>>();

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
        <Text style={styles.headerTitle}>Practice Day</Text>
        <TouchableOpacity style={styles.headerIcon}>
          <Ionicons name="football" size={24} color="black" />
        </TouchableOpacity>
      </View>
      <View style={styles.headerLine} />
      <Image style={styles.image} source={{ uri: card.image }} />
      <View style={styles.cardContent}>
        <View style={styles.infoLeft}>
          <Text style={styles.infoTitle}>{card.title}</Text>
          <View style={styles.location}>
            <Ionicons name="location-sharp" size={16} color="black" />
            <Text style={styles.locationText}>{card.location}</Text>
          </View>
          <Text style={styles.time}>{card.time}</Text>
          <Text style={styles.date}>{card.date}</Text>
        </View>
        <View style={styles.infoRight}>
          <View style={styles.heartIcon}>
            <Ionicons name="heart" size={20} color="blue" />
          </View>
          <View style={styles.stats}>
            <Text style={styles.statNumber}>1/5</Text>
            <Text style={styles.statNumber}>+0 others</Text>
          </View>
        </View>
      </View>
    </View>
  );

  return (
    <View style={styles.container}>
     <SearchBar/>
     <ActionButtons/>
      <Swiper
        cards={cards}
        renderCard={renderCard}
        onSwipedLeft={onSwipedLeft}
        onSwipedRight={onSwipedRight}
        onTapCard={() =>navigation.navigate('(routes)/detail')}
        backgroundColor={'transparent'}
        stackSize={1}
        cardIndex={0}
        infinite={false}
        verticalSwipe={false}
        horizontalSwipe={true}
        containerStyle={styles.swiperContainer}
        cardStyle={styles.cardStyle}
      />
      <ActionIcons onSwipedLeft={onSwipedLeft} onSwipedRight={onSwipedRight}/>
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