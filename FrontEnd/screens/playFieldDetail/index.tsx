import React, { useEffect, useState } from 'react';
import { View, Text, ScrollView, TextInput, StyleSheet, TouchableOpacity, FlatList } from 'react-native';
import { NativeStackNavigationProp } from 'react-native-screens/lib/typescript/native-stack/types';
import { RouteProp, useNavigation } from '@react-navigation/native';
import axios from 'axios';
import { RootStackParamList } from '@/types/types';

type PlayfieldDetailScreenRouteProp = RouteProp<RootStackParamList, 'PlayFieldDetail'>;
type PlayfieldDetailScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'PlayFieldDetail'>;

interface PlayfieldDetailScreenProps {
  route: PlayfieldDetailScreenRouteProp;
}

const PlayFieldDetailCard = ({ route }: PlayfieldDetailScreenProps) => {
  const navigation = useNavigation<PlayfieldDetailScreenNavigationProp>();
  const { playfieldId } = route.params;

  const [year, setYear] = useState<number>(2024); // Current year management
  const [inputYear, setInputYear] = useState<string>(year.toString()); // New year input
  const [revenues, setRevenues] = useState<any[]>([]); // Revenue management
  const [totalRevenue, setTotalRevenue] = useState<number>(0); // Total revenue

  // Function to fetch revenue data
  const fetchRevenueData = async (selectedYear: number) => {
    try {
      const response = await axios.get(
        `https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/bookings/${playfieldId}/${selectedYear}`
      );

      const data = response.data;
      setRevenues(data.monthlyRevenues);
      setTotalRevenue(data.totalRevenue);
    } catch (error) {
      console.error('Error fetching revenue data:', error);
    }
  };

  // Fetch data for the initial year
  useEffect(() => {
    fetchRevenueData(year);
  }, [year]);

  const formatPrice = (price: number) => {
    return price.toLocaleString('vi-VN', {
      style: 'currency',
      currency: 'VND',
    });
  };

  return (
    <View style={styles.container}>
      <Text style={styles.headerTitle}>Revenue PlayField</Text>

      {/* Input to change year */}
      <TextInput
        style={styles.yearInput}
        keyboardType="numeric"
        value={inputYear}
        onChangeText={(text) => {
          setInputYear(text); // Update input year value
        }}
      />

      {/* Button to update the year and fetch revenue */}
      <TouchableOpacity
        style={styles.updateButton}
        onPress={() => {
          const parsedYear = parseInt(inputYear);
          if (!isNaN(parsedYear)) {
            setYear(parsedYear); // Update state with new year value
            fetchRevenueData(parsedYear); // Fetch revenue data for the new year
          }
        }}
      >
        <Text style={styles.updateButtonText}>Update</Text>
      </TouchableOpacity>

      {/* Display revenue statistics */}
      <View style={styles.revenueContainer}>
        <Text style={styles.totalRevenueText}>Tổng doanh thu: {formatPrice(totalRevenue)}</Text>

        {/* Header row */}
        <View style={styles.headerRow}>
          <Text style={styles.headerText}>Tháng</Text>
          <Text style={styles.headerText}>Doanh thu (VNĐ)</Text>
        </View>

        {/* Use FlatList to render revenue */}
        <FlatList
          data={revenues}
          keyExtractor={(item) => item.month.toString()}
          renderItem={({ item }) => (
            <View style={styles.row}>
              <Text style={styles.rowText}>Tháng {item.month}</Text>
              <Text style={styles.rowText}>{formatPrice(item.revenue)}</Text>
            </View>
          )}
          ListEmptyComponent={<Text style={styles.rowText}>No revenue data available.</Text>}
        />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f9f9f9',
    paddingTop: 40,
  },
  headerTitle: {
    fontSize: 24,
    color: '#333',
    fontWeight: 'bold',
    textAlign: 'center',
  },
  yearInput: {
    borderWidth: 1,
    borderColor: '#ccc',
    padding: 8,
    borderRadius: 4,
    width: 80,
    margin: 16,
    alignSelf: 'center',
  },
  revenueContainer: {
    padding: 16,
  },
  totalRevenueText: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 8,
  },
  headerRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    borderBottomWidth: 2,
    borderBottomColor: '#ccc',
    paddingBottom: 8,
    marginBottom: 8,
  },
  headerText: {
    fontSize: 16,
    fontWeight: 'bold',
    width: '50%', // Ensure equal space for columns
    textAlign: 'center',
  },
  row: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
  },
  rowText: {
    fontSize: 16,
    width: '50%', // Ensure equal space for columns
    textAlign: 'center',
  },
  updateButton: {
    backgroundColor: '#48C9B0',
    borderRadius: 8,
    paddingVertical: 10,
    paddingHorizontal: 20,
    alignItems: 'center',
    margin: 16,
  },
  updateButtonText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },
});

export default PlayFieldDetailCard;
