import logo from './logo.svg';
import './App.css';
import { AppBar, Box, Card, CardContent, Grid, Paper, Toolbar, Typography } from '@mui/material';
import { TreeControl } from './TreeControl.tsx';

function App() {
  return (
    <div className="App">
<Box sx={{ height: '98vh' }}>
<Box sx={{ flexGrow: 1 }}>
      <AppBar sx={{ backgroundColor: '#bbbbbb' }} >
      <Toolbar variant='dense' />
      </AppBar>
      <Toolbar variant='dense' />
    </Box>
      <Grid container sx={{ height: 'calc(100% - 60px)' }}>
      
        <Grid item xs={3} sx={{ height: "100%"}}>
            
              <TreeControl/>
           

          </Grid>

          <Grid item xs sx={{ minWidth: "100px" }}>

            <Box sx={{ height: '100%' }}>
              
            </Box>

          </Grid>

        <Grid item xs={3} sx={{ height: "100%"}}>
          <Paper sx={{ height: "100%", overflow: 'auto', width: "100%" }}>
            
            </Paper>

          </Grid>
        </Grid>
    </Box>
    </div>
  );
}

export default App;
