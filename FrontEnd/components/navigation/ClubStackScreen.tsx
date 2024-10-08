import { createNativeStackNavigator } from '@react-navigation/native-stack';
import ViewAllClubsScreen from '../../screens/Club/ViewAllClubsScreen';
import YourClubScreen from '../../screens/Club/YourClubScreenn';
import ClubDetailScreen from '../../screens/Club/ClubDetailScreen';

const ClubStack = createNativeStackNavigator();

const ClubStackScreens = () => {
  return (
    <ClubStack.Navigator>
      <ClubStack.Screen name="ViewAllClubs" component={ViewAllClubsScreen} options={{ title: 'All Club' }} />
      <ClubStack.Screen name="YourClub" component={YourClubScreen} options={{ title: 'Your Club' }} />
      <ClubStack.Screen name="ClubDetail" component={ClubDetailScreen} options={{ title: 'Club Detail' }} />
    </ClubStack.Navigator>
  );
};

export default ClubStackScreens;