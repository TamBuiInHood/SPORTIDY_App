import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { Card } from 'react-native-paper'; // Assuming you're using react-native-paper
import Ionicons from 'react-native-vector-icons/Ionicons'; // Icon package

interface StatCardProps {
  sport: string;
  bookings: number;
  occupancy: string;
  cancel: number;
}

const StatCard: React.FC<StatCardProps> = ({ sport, bookings, occupancy, cancel }) => {
  // Map to associate sports with their icons
  const sportIcons: { [key: string]: string } = {
    Football: 'football-outline',
    Badminton: 'baseball-outline',
    Pickleball: 'tennisball-outline', // Customize icons as needed
  };

  // Choose the corresponding icon based on the sport
  const iconName = sportIcons[sport] || 'help-outline'; // Default to 'help-outline' if no match

  return (
    <Card style={styles.card}>
      <Card.Content>
        <View style={styles.cardContent}>
          <Ionicons name={iconName} style={styles.icon} />
          
          {/* Sport information */}
          <View style={styles.textContent}>
            <Text style={styles.sportName}>{sport}</Text>
            <View style={styles.statsRow}>
              <Text style={styles.label}>Field bookings</Text>
              <Text style={styles.bookingsValue}>{bookings}</Text>
            </View>
            <View style={styles.statsRow}>
              <Text style={styles.label}>Occupancy rate</Text>
              <Text style={styles.occupancyValue}>{occupancy}</Text>
            </View>
            <View style={styles.statsRow}>
              <Text style={styles.label}>Cancel</Text>
              <Text style={styles.cancelValue}>{cancel}</Text>
            </View>
          </View>
        </View>
      </Card.Content>
    </Card>
  );
};

// Styles to match the design
const styles = StyleSheet.create({
  card: {
    borderRadius: 12,
    marginVertical: 8,
    backgroundColor: '#fff',
    shadowColor: '#000',
    shadowOpacity: 0.1,
    shadowRadius: 10,
    elevation: 3,
  },
  cardContent: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  icon: {
    fontSize: 32,
    color: '#48C9B0',
    marginRight: 12,
  },
  textContent: {
    flex: 1,
  },
  sportName: {
    fontSize: 16,
    fontWeight: 'bold',
    marginBottom: 8,
    color: '#333',
  },
  statsRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 4,
  },
  label: {
    fontSize: 14,
    color: '#666',
    marginVertical: 5
  },
  bookingsValue: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#333',
  },
  occupancyValue: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#333',
  },
  cancelValue: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#E67E22', 
  },
});

export default StatCard;
