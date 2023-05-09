import logo from './logo.svg';
import './App.css';
import { AppBar, Box, Card, CardContent, Grid, Paper, Toolbar, Typography } from '@mui/material';
import { TreeControl } from './TreeControl.tsx';

function App() {
  return (
      <Box sx={{ height: '98vh' }}>
        <Box sx={{ height:'111px', backgroundColor: '#aabbbb' }}>
          APPBAR
          </Box> 
        <Grid 
        container        
         sx={{
            width: '100%',            
            height: 'calc(100% - 111px)' 
            }}>

          <Grid item xs={3} sx={{
           height: '100%'      
             }}>
            <TreeControl />
          </Grid>

          <Grid item xs sx={{ minWidth: "100px", height:'50%' }}>
            <Box sx={{
            width: '99%',            
            height: '50%',
            border:1
            }}>
              BOX
            </Box>
          </Grid>

          <Grid item xs={3} sx={{ height: '50%'}}>
            <Box sx={{
            width: '99%',            
            height: '50%',
            border:1
            }}>BOX2</Box>
          </Grid>
        </Grid>
      </Box>
  );
}

export default App;
