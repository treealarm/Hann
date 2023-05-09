import logo from './logo.svg';
import './App.css';
import { Box, Card, CardContent, Grid } from '@mui/material';
import { TreeControl } from './TreeControl.tsx';

function App() {
    return (
        <Box sx={{ height: '100vh', display: 'flex', flexDirection: 'column' }}>

            <Box sx={{
                width: '100%',
                height: '77px',
                backgroundColor: '#aabbbb'
            }}
            >
                APPBAR
            </Box>
 
                <Grid container sx={{ 
                    height: '100%',
                    width: '100%',
                    overflow: 'auto',
                    flex: 1
                     }}>
                    <Grid item xs={3} sx={{ height: "99%" }}>
                        <TreeControl />
                    </Grid>
                    <Grid item xs sx={{ minWidth: "100px" }}>
                        <Box sx={{ height: '99%', border:1}}>
                            BOX
                        </Box>
                    </Grid>

                    <Grid item xs={3} sx={{ height: "99%"}}>
                        <Box sx={{ 
                            height: "100%",
                             overflow: 'auto',
                              width: "99%",
                              border:1 
                              }}>
                            PAPER
                        </Box>
                    </Grid>
                </Grid>
        </Box>
    );
}

export default App;
