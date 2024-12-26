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
import { Card, MeetingsResponse, RootStackParamList } from '@/types/types';
import YourMeeting from '@/screens/home/yourMeet';

type HomeScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "HomeScreen">;

const HomeScreen: React.FC = () => {
  const [cards, setCards] = useState<Card[]>([]);
  const [likedCards, setLikedCards] = useState<Card[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const navigation = useNavigation<HomeScreenNavigationProp>();
  const [activeTab, setActiveTab] = useState<'available' | 'meet'>('available');
  const getSportIcon = (sportId: number) => {
    switch (sportId) {
      case 1:
        return 'football';
      case 2:
        return 'basketball';
      case 3:
        return 'badminton';
      default:
        return 'football';
    }
  }
  const fetchMeetings = async () => {
    try {
      const response = await api.getAllMeeting(0, 20);
      console.log('response:', response);
      console.log('response.data:', response.data);
      const result: MeetingsResponse = response.data;
      console.log('result:', result);

      if (result && result.list && Array.isArray(result.list)) {
        setCards(result.list);
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
    fetchMeetings();
  }, []);

  const onSwipedLeft = (cardIndex: number) => {
    console.log('Skipped:', cards[cardIndex]);
  };

  const onSwipedRight = (cardIndex: number) => {
    setLikedCards([...likedCards, cards[cardIndex]]);
    console.log('Liked:', cards[cardIndex]);
  };

  const renderCard = (card: Card, index: number) => (
    <View style={styles.card}>
      <View style={styles.cardHeader}>
        <Text style={styles.headerTitle}>Meeting</Text>
        <TouchableOpacity style={styles.headerIcon}>
          <Ionicons name={getSportIcon(card.sportId)} size={24} color="black" />
        </TouchableOpacity>
      </View>
      <View style={styles.headerLine} />
      <Image style={styles.image} source={{ uri: card.meetingImage }} />
      <View style={styles.cardContent}>
        <View style={styles.infoLeft}>
          <Text style={styles.infoTitle}>{card.meetingName || 'Unnamed Meeting'}</Text>
          <View style={styles.location}>
            <Ionicons name="location-sharp" size={16} color="black" />
            <Text style={styles.locationText}>{card.address}</Text>
          </View>
          <Text style={styles.time}>{new Date(card.startDate).toLocaleTimeString()}</Text>
          <Text style={styles.date}>{new Date(card.startDate).toLocaleDateString()}</Text>
        </View>
        <View style={styles.infoRight}>
          <View style={styles.heartIcon}>
            <Ionicons name="heart" size={20} color="blue" />
          </View>
          <View style={styles.stats}>
            <Text style={styles.statNumber}>{`${card.host}/${card.totalMember}`}</Text>
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
      <View style={styles.buttonContainer}>
        <TouchableOpacity
          style={[styles.meetButton, activeTab === 'available' && styles.activeTabButton]}
          onPress={() => setActiveTab('available')}
        >
          <Text style={[styles.meetButtonText, activeTab === 'available' && styles.activeTabText]}>
            Available
          </Text>
        </TouchableOpacity>

        <TouchableOpacity
          style={[styles.meetButton, activeTab === 'meet' && styles.activeTabButton]}
          onPress={() => setActiveTab('meet')}
        >
          <Text style={[styles.meetButtonText, activeTab === 'meet' && styles.activeTabText]}>
            Your Meet
          </Text>
        </TouchableOpacity>
      </View>
      <View>
        {activeTab === 'available' && (
          cards.length > 0 ? (
            <Swiper
              cards={cards}
              renderCard={renderCard}
              onSwipedLeft={onSwipedLeft}
              onSwipedRight={onSwipedRight}
              onTapCard={(index) => navigation.navigate('EventDetail', { meetingId: cards[index].meetingId })}
              backgroundColor={'transparent'}
              stackSize={1}
              cardIndex={0}
              infinite={false}
              verticalSwipe={false}
              horizontalSwipe={true}
              cardStyle={styles.cardStyle}
            />
          ) : (
            <Text>No meetings available</Text>
          )
        )}
      </View>
      {activeTab === 'meet' && <YourMeeting />}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'column',
    backgroundColor: '#fff',
    marginTop: 50
  },
  buttonContainer: {
    flexDirection: 'row', 
    justifyContent: 'space-around', 
    marginTop: 100
  },
  meetButton: {
    backgroundColor: '#fff',
    borderWidth: 1,
    borderColor: '#FF915D',
    paddingVertical: 10,
    paddingHorizontal: 30,
    borderRadius: 50,
  },
  meetButtonText: {
    color: '#FF915D',
    fontSize: 16,
  },
  activeTabButton: {
    backgroundColor: '#FF915D',
  },
  activeTabText: {
    color: '#fff',
  },
  swiper: {
    flex: 1,
    justifyContent: 'center',
    backgroundColor: 'transparent',
  },
  card: {
    width: '90%',
    height: '60%',
    borderRadius: 30,
    borderWidth: 1,
    borderColor: '#ddd',
    alignSelf: 'center',
    backgroundColor: '#fff',
    padding: 10,
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