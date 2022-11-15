import styled from 'styled-components';

export const Month = styled.div`
  > .header {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 1em;
    cursor: pointer;

    &:hover {
      color: ${(props) => props.theme.css.primaryLightColor};
      background-color: ${(props) => props.theme.css.light};
    }

    h1 {
      margin: 0;
    }
  }

  hr {
    width: 100%;
    color: ${(props) => props.theme.css.primaryColor};

    &:hover {
      color: ${(props) => props.theme.css.primaryLightColor};
    }
  }

  .events {
    display: flex;
    flex-direction: column;
    gap: 1em;

    &.hide {
      display: none;
    }
  }
`;
