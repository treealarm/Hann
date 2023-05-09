import logo from './logo.svg';
import './App.css';
import { Box, Card, CardContent, Grid } from '@mui/material';
import { TreeControl } from './TreeControl.tsx';

function App() {
    return (
        <Box sx={{ height: '98vh', display: 'flex', flexDirection: 'column' }}>

            <Box sx={{
                width: '100%',
                height: '100px',
                backgroundColor: '#aabbbb'
            }}
            >
                APPBAR
            </Box>
            <Box
                sx={{
                    width: '100%',
                    overflow: 'auto',
                    flex: 1
                }}
            >
                <Grid container sx={{ height: '100%' }}>
                    <Grid item xs={3} sx={{ height: "100%" }}>
                        <TreeControl />
                    </Grid>
                    <Grid item xs sx={{ minWidth: "100px" }}>
                        <Box sx={{ height: '100%'}}>
                            BOX
                        </Box>
                    </Grid>

                    <Grid item xs={3} sx={{ height: "100%"}}>
                        <Box sx={{ height: "100%", overflow: 'auto', width: "100%" }}>
                            PAPER
                        </Box>
                    </Grid>
                </Grid>
            </Box>
        </Box>
    );
}

export default App;
