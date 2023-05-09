import logo from './logo.svg';
import './App.css';
import { Box, Card, CardContent} from '@mui/material';
import { TreeControl } from './TreeControl.tsx';

function App() {
    return (
        <Box sx={{ height: '98vh', display:'flex', flexDirection:'column' }}>

            <Box sx={{ 
                width: '100%',
                 height: '100px',
                 backgroundColor: '#aabbbb' }}
            >
                APPBAR
            </Box>
            <Box
                sx={{
                    width: '100%',                   
                    overflow: 'auto',
                    flex:1
                }}
            >
                <Card sx={{ height: '2000px', backgroundColor: 'lightgreen' }}>
                    <CardContent>LIST</CardContent>
                </Card>
            </Box>
        </Box>
    );
}

export default App;
