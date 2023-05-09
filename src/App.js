import logo from './logo.svg';
import './App.css';
import { AppBar, Box, Card, CardContent, Grid, Paper, Toolbar, Typography } from '@mui/material';
import { TreeControl } from './TreeControl.tsx';

function App() {
  return (
    <div className="App">
      <Box sx={{ height: '100vh' }}>
        <Box sx={{ flexGrow: 1 }}>
          <AppBar sx={{ backgroundColor: '#bbbbbb' }} >
            <Toolbar variant='dense'>APPBAR</Toolbar>
          </AppBar>
          <Toolbar variant='dense'>FAKE</Toolbar>
        </Box>
        <Grid container sx={{ height: 'calc(100% - 60px)' }}>
          <Grid item xs={3} sx={{ height: "100%" }}>
            <TreeControl />
          </Grid>
          <Grid item xs sx={{ minWidth: "100px" }}>
            <Box sx={{ height: '100%', border:1 }}>
              BOX
            </Box>
          </Grid>

          <Grid item xs={3} sx={{ height: "100%", border:1 }}>
            <Paper sx={{ height: "100%", overflow: 'auto', width: "100%" }}>
              PAPER
            </Paper>
          </Grid>
        </Grid>
      </Box>
    </div>
  );
}

export default App;
