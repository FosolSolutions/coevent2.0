import './index.scss';

import { AppRouter, Layout } from 'components';
import { PadlockProvider, Settings } from 'hooks';
import { CookiesProvider } from 'react-cookie';
import { BrowserRouter } from 'react-router-dom';
import ReactTooltip from 'react-tooltip';

function App() {
  const name = 'CoEvent';

  return (
    <CookiesProvider>
      <PadlockProvider
        oidc={{
          token: '/auth/token',
        }}
        baseApiUrl={Settings.ApiPath}
      >
        <BrowserRouter basename={process.env.PUBLIC_URL}>
          <Layout name={name}>
            <AppRouter />
          </Layout>
        </BrowserRouter>

        <ReactTooltip id="main-tooltip" effect="float" type="light" place="top" />
        <ReactTooltip id="main-tooltip-right" effect="solid" type="light" place="right" />
      </PadlockProvider>
    </CookiesProvider>
  );
}

export default App;
