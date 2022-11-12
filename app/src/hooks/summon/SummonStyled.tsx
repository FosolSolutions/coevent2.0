import styled from 'styled-components';

export const Summon = styled.div`
  display: none;
  background-color: black;
  opacity: 0.35;
  z-index: 100;
  position: absolute;
  top: 0;
  width: 100%;
  height: 100%;

  &.show {
    display: block;
  }
`;
