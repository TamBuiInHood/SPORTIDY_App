import React, { useEffect, useState } from 'react';
import { View, Text, ScrollView, StyleSheet, Image, ActivityIndicator } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

interface Event {
  id: number;
  date: string;
  time: string;
  title: string;
  location: string;
  club: string;
  participants: number;
  maxParticipants: number;
}

const YourMeeting = () => {
  const [events, setEvents] = useState<Event[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchMeetings = async () => {
      try {
        const userId = await AsyncStorage.getItem('userId');
        if (!userId) {
          setError('User not found.');
          setLoading(false);
          return;  // Stop execution if userId is not found
        }
  
        const response = await axios.get(
          `https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/meetings/get-all-meeting-of-user/${userId}`
        );
  
        console.log('API response:', response.data);  // Log the response
  
        const fetchedData = response.data.data;  // Correctly access the meetings array
  
        // Check if fetchedData is an array before calling .map()
        if (!Array.isArray(fetchedData)) {
          setError('Unexpected response format');
          return;
        }
  
        const formattedData = fetchedData.map((meeting: any) => ({
          id: meeting.meetingId,
          date: new Date(meeting.startDate).toLocaleDateString(),
          time: new Date(meeting.startDate).toLocaleTimeString(),
          title: meeting.meetingName,
          location: meeting.address,
          club: meeting.clubName || 'NO CLUB',
          participants: meeting.totalMember,
          maxParticipants: 10,
        }));
  
        setEvents(formattedData);
      } catch (err) {
        console.error('Error fetching meetings:', err);
        setError('Failed to fetch meetings.');
      } finally {
        setLoading(false);
      }
    };
  
    fetchMeetings();
  }, []);

  if (loading) {
    return <ActivityIndicator size="large" color="#0000ff" />;
  }

  if (error) {
    return <Text>{error}</Text>;
  }
  return (
    <ScrollView style={styles.container}>
      {events.map((event) => (
        <View key={event.id} style={styles.card}>
          <View style={styles.dateContainer}>
            <Text style={styles.dateText}>{event.date}</Text>
            <Text style={styles.timeText}>{event.time}</Text>
          </View>

          <View style={styles.detailsContainer}>
            <View style={styles.clubContainer}>
              {event.title.toLowerCase().includes('football') && (
                <Ionicons name="football-outline" color={'#ffd591'} size={30} />
              )}
              {event.title.toLowerCase().includes('pickleball') && (
                <Ionicons name="football-outline" color={'#ffd591'} size={30} />
              )}
              {event.title.toLowerCase().includes('volleyball') && (
                <Ionicons name="football-outline" color={'#ffd591'} size={30} />
              )}

              <Text style={styles.clubText}>{event.club}</Text>
              <Text style={styles.hostedText}>Hosted</Text>
            </View>

            <Text style={styles.titleText}>{event.title}</Text>
            <Text style={styles.locationText}>{event.location}</Text>

            <View style={styles.participantsContainer}>
              <Text>{`${event.participants}/${event.maxParticipants}`}</Text>
              <View style={styles.iconsContainer}>
                {Array.from({ length: Math.min(event.participants, 5) }).map((_, index) => (
                  <Image
                    key={index}
                    source={{
                      uri: 'https://i.pinimg.com/564x/40/98/2a/40982a8167f0a53dedce3731178f2ef5.jpg',
                    }}
                    style={styles.participantImage}
                  />
                ))}

                {event.participants > 5 && <Text>+others</Text>}
              </View>
            </View>
          </View>
        </View>
      ))}
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10,
    backgroundColor: '#f9f9f9',
    marginBottom: 100,
  },
  card: {
    backgroundColor: '#fff',
    borderRadius: 10,
    padding: 15,
    marginBottom: 20,
    elevation: 2,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 5,
  },
  dateContainer: {
    alignItems: 'center',
  },
  dateText: {
    fontSize: 14,
    fontWeight: 'bold',
  },
  timeText: {
    fontSize: 12,
    color: '#555',
  },
  detailsContainer: {
    marginTop: 10,
  },
  clubContainer: {
    flexDirection: 'row',
  },
  clubText: {
    marginTop: 7,
    fontSize: 14,
    fontWeight: 'bold',
    marginLeft: 10,
  },
  hostedText: {
    fontSize: 12,
    color: '#f39c12',
    marginLeft: 10,
    borderColor: '#f39c12',
    borderWidth: 1,
    borderRadius: 10,
    padding: 5,
  },
  titleText: {
    fontSize: 16,
    fontWeight: 'bold',
    marginVertical: 5,
  },
  participantImage: {
    width: 30,
    height: 30,
    borderRadius: 15,
    marginRight: 5,
  },
  participantsContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 10,
  },
  iconsContainer: {
    flexDirection: 'row',
  },
});

export default YourMeeting;
