import styled from 'styled-components';

export const Layout = styled.div`
  display: flex;
  flex-direction: column;
  align-items: stretch;
  height: 100vh;

  .main-window {
    display: flex;
    flex-direction: row;
    align-items: stretch;
    flex-grow: 1;
  }

  header {
    padding: 0.5em;
    display: flex;
    flex-direction: row;
    align-items: flex-end;
    gap: 1em;
    justify-content: flex-end;
  }

  main {
    flex-grow: 1;
    overflow: auto;
    position: relative;
  }
`;

export default Layout;
