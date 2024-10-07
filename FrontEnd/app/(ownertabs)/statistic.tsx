import { ScrollView, StyleSheet } from 'react-native';
import React from 'react';
import RevenueChart from '@/components/RevenueChart';
import StatCard from '@/components/StatCard';
import { LinearGradient } from 'expo-linear-gradient';

const Statistic: React.FC = () => {
  return (
    <LinearGradient
      colors={['#76B852', '#1ABC9C']}
      style={styles.container}
    >
      <ScrollView contentContainerStyle={styles.content}>
        <RevenueChart />
        <StatCard sport="Football" bookings={10} occupancy="90%" cancel={1} />
        <StatCard sport="Baseball" bookings={8} occupancy="80%" cancel={2} />
        <StatCard sport="Pickleball" bookings={5} occupancy="100%" cancel={0} />
      </ScrollView>
    </LinearGradient>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,  
  },
  content: {
    marginTop: 30,
    padding: 10,
  },
});

export default Statistic;
