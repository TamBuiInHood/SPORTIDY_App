import React, { useState, useEffect } from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import api from '@/config/api';
import { Sport } from '@/types/types';

interface SportChooseProps {
  setSport: (sportId: number | null) => void;
}

const SportChoose: React.FC<SportChooseProps> = ({ setSport }) => {
  const [sports, setSports] = useState<Sport[]>([]);
  const [selectedSport, setSelectedSport] = useState<number | null>(null);

  useEffect(() => {
    const getAllSports = async () => {
      try {
        const response = await api.getAllSports();
        setSports(response.data);
      } catch (error) {
        console.error(error);
      }
    };
    getAllSports();
  }, []);

  const handleSportSelect = (sportId: number) => {
    setSelectedSport(sportId);
    setSport(sportId);
  };

  return (
    <View>
      <Text style={styles.sectionTitle}>SPORT</Text>

      <View style={styles.sportChoices}>
        {sports.map((sport) => (
          <TouchableOpacity
            key={sport.sportId}
            style={[
              styles.sportOption,
              selectedSport === sport.sportId && styles.sportOptionSelected,
            ]}
            onPress={() => handleSportSelect(sport.sportId)}
          >
            <Ionicons
              name={
                sport.sportId === 1
                  ? 'football-outline'
                  : sport.sportId === 2
                  ? 'basketball-outline'
                  : 'baseball'
              }
              size={30}
              color={selectedSport === sport.sportId ? '#fff' : '#000'}
            />
            <Text
              style={[
                styles.sportOptionText,
                selectedSport === sport.sportId && styles.sportOptionTextSelected,
              ]}
            >
              {sport.sportName}
            </Text>
          </TouchableOpacity>
        ))}
      </View>
    </View>
  );
};
const styles= StyleSheet.create({
    sectionTitle: {
        fontSize: 18,
        fontWeight: 'bold',
        padding: 16,
        color: '#ff951d'
      },
      sportChoices: {
        flexDirection: 'row',
        justifyContent: 'space-around',
        width: '100%',
    
      },
      sportOption: {
        flexDirection: 'column',
        alignItems: 'center',
        padding: 10,
        borderWidth: 1,
        borderColor: '#ff951d',
        borderRadius: 20,
        width: 120,
        height: 80,
      },
      sportOptionSelected: {
        backgroundColor: '#ff951d',
        color: '#ffff'
      },
      sportOptionText: {
        marginTop: 5,
        fontSize: 14,
        color: 'black',
      },
      sportOptionTextSelected: {
        color: '#ffff',
        fontWeight: 'bold'
      },
})

export default SportChoose