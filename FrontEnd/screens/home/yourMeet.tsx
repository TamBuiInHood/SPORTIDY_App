import ActionButtons from '@/components/ActionButton';
import SearchBar from '@/components/SearchBar';
import { Ionicons } from '@expo/vector-icons';
import React from 'react';
import { View, Text, ScrollView, StyleSheet, TouchableOpacity, Image } from 'react-native';

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
  const mockData: Event[] = [
    {
      id: 1,
      date: '08/06/2024',
      time: '3:30 PM',
      title: 'Football training with Coach TanHuynh',
      location: 'Phu Giao, Binh Duong',
      club: 'THE TAM CLUB',
      participants: 5,
      maxParticipants: 5,
    },
    {
      id: 2,
      date: '08/06/2024',
      time: '6:00 PM',
      title: 'Pickleball Training',
      location: 'District 9, TPHCM',
      club: 'NO CLUB',
      participants: 10,
      maxParticipants: 10,
    },
    {
      id: 3,
      date: '09/06/2024',
      time: '5:00 AM',
      title: 'Volleyball training with Coach TanHuynh',
      location: 'District 1, TPHCM',
      club: 'THE TAM CLUB',
      participants: 1,
      maxParticipants: 5,
    },
  ];

  return (
    <ScrollView style={styles.container}>

      {mockData.map((event) => (
        <View key={event.id} style={styles.card}>
          <View style={styles.dateContainer}>
            <Text style={styles.dateText}>{event.date}</Text>
            <Text style={styles.timeText}>{event.time}</Text>
          </View>

          <View style={styles.detailsContainer}>
            <View style={styles.clubContainer}>
              {event.title.toLowerCase().includes('football') && (
                <Ionicons name='football-outline' color={'#ffd591'} size={30}
                />
              )}
              {event.title.toLowerCase().includes('pickleball') && (
               <Ionicons name='football-outline' color={'#ffd591'} size={30}
               />
              )}
              {event.title.toLowerCase().includes('volleyball') && (
               <Ionicons name='football-outline' color={'#ffd591'} size={30}
               />
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

                {event.participants > 5 && (
                  <Text>+others</Text> 
                )}
              </View>

            </View>
          </View>
        </View>
      ))
      }
    </ScrollView >
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10,
    backgroundColor: '#f9f9f9',
    marginBottom: 100
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingTop: 150,
  },
  locationText: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  notificationButton: {
    backgroundColor: '#f1f1f1',
    padding: 10,
    borderRadius: 20,
  },
  availableText: {
    fontSize: 18,
    fontWeight: 'bold',
    marginVertical: 10,
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
    marginLeft: 10
  },
  hostedText: {
    fontSize: 12,
    color: '#f39c12',
    marginLeft: 10,
    borderColor: '#f39c12',
    borderWidth: 1,
    borderStyle: 'solid',
    borderRadius: 10,
    padding: 5
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
