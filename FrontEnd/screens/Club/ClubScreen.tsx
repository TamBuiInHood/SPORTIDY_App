import { useNavigation } from '@react-navigation/native';
import { useEffect } from 'react';
import { RootStackParamList } from '@/types/types';

import { RouteProp } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
type ClubScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'Club'>;
type ClubScreenRouteProp = RouteProp<RootStackParamList, 'Club'>;

const ClubScreen = ({ navigation, route }: { navigation: ClubScreenNavigationProp; route: ClubScreenRouteProp }) => {
  useEffect(() => {
    navigation.navigate('ViewAllClubs'); // Navigate đến màn hình đầu tiên trong Club stack
  }, [navigation]);

  return null;
};

export default ClubScreen;